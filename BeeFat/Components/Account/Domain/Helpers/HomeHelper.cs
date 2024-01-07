using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.ProgressBar;

namespace BeeFat.Helpers;

public class HomeHelper
{
    private Guid _id = FakeData.HardId;

    public Modal SelectEatenFoodWindow = default!;
    
    public JournalFood SelectedJournalFood;
    public int RightPortionSize;
    public int PortionSize;
    public ApplicationUser User;
    public Macronutrient TodayMacronutrient;
    public UserRepository UserRepository;
    public JournalRepository JournalRepository;
    public JournalFoodRepository JournalFoodRepository;
    
    public HomeHelper(UserRepository userRepository, JournalRepository journalRepository, JournalFoodRepository journalFoodRepository)
    {
        JournalFoodRepository = journalFoodRepository;
        JournalRepository = journalRepository;
        UserRepository = userRepository;
        User = UserRepository.GetById(_id);
        TodayMacronutrient = new Macronutrient();
        SelectEatenFoodWindow = new Blazorise.Bootstrap.Modal();
        SelectEatenFoodWindow.Hide();
        SelectEatenFoodWindow.Visibility = Visibility.Invisible;
        SelectEatenFoodWindow.Visible = false;
        
    }

    public void ShowModalWindow(JournalFood journalFood)
    {
        SelectedJournalFood = journalFood;
        SelectEatenFoodWindow.Show();
    }

    public void ChangeFoodProductInfoAndSave(bool isEaten)
    {
        SelectedJournalFood.PortionSize = PortionSize;
        SelectedJournalFood.IsEaten = isEaten;
        JournalFoodRepository.Update(SelectedJournalFood);
    }

    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<JournalFood> fpsource)
    {
        return GetTotalMacronutrientsByDay(fpsource, _ => true, StaticBeeFat.Today);
    }
    
    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<FoodProduct> fpsource)
    {
        return GetTotalMacronutrientsByDay(fpsource, _ => true, StaticBeeFat.Today);
    }

    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<JournalFood> fpsource, Func<JournalFood, bool> selector,  DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        foreach (var product in GetProductsByDay(fpsource, dayOfWeek))
        {
            if (selector.Invoke(product)) 
                totalMacronutrients += product.Macronutrient * product.PortionCoeff;
        }
        // TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return totalMacronutrients;
    }
    
    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<FoodProduct> fpsource, Func<FoodProduct, bool> selector,  DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        foreach (var product in GetProductsByDay(fpsource, dayOfWeek))
        {
            if (selector.Invoke(product)) 
                totalMacronutrients += product.Food.Macronutrient * product.PortionCoeff;
        }
        TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return totalMacronutrients;
    }

    public IEnumerable<IEnumerable<JournalFood>> GetNextDaysFoodProducts(IEnumerable<JournalFood> fpSource, DayOfWeek start)
    {
        var todayNumber = (int)start;
        for (var dayNumber = todayNumber + 1; dayNumber <= 6 && dayNumber < todayNumber + 3; dayNumber++)
        {
            var products = fpSource.Where(p => (int)p.DayOfWeek == dayNumber).ToList();
            if (!products.Any()) break;
            yield return products;
        }
    }

    public IEnumerable<JournalFood> GetProductsByDay(IEnumerable<JournalFood> fpSource)
    {
        return GetProductsByDay(fpSource, StaticBeeFat.Today);
    }
    
    public IEnumerable<FoodProduct> GetProductsByDay(IEnumerable<FoodProduct> fpSource)
    {
        return GetProductsByDay(fpSource, StaticBeeFat.Today);
    }

    public IEnumerable<JournalFood> GetProductsByDay(IEnumerable<JournalFood> fpSource, DayOfWeek dayOfWeek)
    {
        return fpSource
            .Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
    }
    
    public IEnumerable<FoodProduct> GetProductsByDay(IEnumerable<FoodProduct> fpSource, DayOfWeek dayOfWeek)
    {
        return fpSource
            .Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
    }

    public ApplicationUser FetchUserInfo()
    {
        return UserRepository.GetById(_id);
    }

    public void Save()
    {
        ChangeFoodProductInfoAndSave(true);  
    }

    public void CloseWindow()
    {
        SelectEatenFoodWindow.Close(CloseReason.UserClosing); 
        Save();        
        var fpSource = User.Journal.FoodProducts;
        GetTotalMacronutrientsByDay(fpSource); 
    }

    public void CancelEatenProduct(JournalFood product)
    {
        product.IsEaten = false;
        product.PortionSize = 0;
        SelectedJournalFood = product; 
        ChangeFoodProductInfoAndSave(false);
    }

    public void SetEatenProduct(JournalFood product)
    {
        // RightPortionSize = fp.PortionSize;
        SelectedJournalFood = product; 
        ShowModalWindow(product);
    }

    public double CalculatePercentage(int firstParam, int secondParam)
    {
        var result = (int)(100 * (firstParam / (double)secondParam));
        if (result < 0) return 0;
        if (result > 100) return 100;
        return result;
    }
    
    public void TextHandler(TextRenderEventArgs args, int totalEatenCalories, int totalCalories)
    {
        args.Text = $"{totalEatenCalories}/{totalCalories}";
    }

    public IEnumerable<DayMacronutrient> GetPrefixWeekMacronutrients(DayOfWeek lastDay = DayOfWeek.Sunday)
    {
        User.Journal = JournalRepository.GetById(User.JournalId);
        // User = UserRepository.GetById(User.Id);
        foreach (var day in StaticBeeFat.GetDays(1, 7))
        {
            yield return new DayMacronutrient(GetTotalMacronutrientsByDay(User.Journal.FoodProducts, f => f.IsEaten, day), day);
        }
    }
}


public class DayMacronutrient
{
    public Macronutrient Macronutrient;
    public DayOfWeek DayOfWeek;

    public DayMacronutrient(Macronutrient macronutrient, DayOfWeek dayOfWeek)
    {
        Macronutrient = macronutrient;
        DayOfWeek = dayOfWeek;
    }
}