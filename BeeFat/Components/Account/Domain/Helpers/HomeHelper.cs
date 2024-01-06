using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;
using Syncfusion.Blazor.ProgressBar;

namespace BeeFat.Components.Account.Domain.Helpers;

public class HomeHelper
{
    public Modal SelectEatenFoodWindow = default!;
    public JournalFood? SelectedFoodProduct;
    public int RightPortionSize;
    public int PortionSize;
    public ApplicationUser User;
    public Macronutrient TodayMacronutrient;
    public UserRepository UserRepository;
    public JournalRepository JournalRepository;
    public JournalFoodRepository FoodProductRepository;
    
    public HomeHelper(UserRepository userRepository, JournalRepository journalRepository, JournalFoodRepository foodProductRepository)
    {
        FoodProductRepository = foodProductRepository;
        JournalRepository = journalRepository;
        UserRepository = userRepository;
        TodayMacronutrient = new Macronutrient();
    }

    public void ShowModalWindow(JournalFood journalFood)
    {
        SelectedFoodProduct = journalFood;
        SelectEatenFoodWindow.Show();
    }

    public void ChangeFoodProductInfoAndSave(bool isEaten)
    {
        SelectedFoodProduct.PortionSize = PortionSize;
        SelectedFoodProduct.IsEaten = isEaten;
        FoodProductRepository.Update(SelectedFoodProduct);
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
        SelectedFoodProduct = product; 
        ChangeFoodProductInfoAndSave(false);
    }

    public void SetEatenProduct(JournalFood product, FoodProduct fp)
    {
        RightPortionSize = fp.PortionSize;
        SelectedFoodProduct = product; 
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
}