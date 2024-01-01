using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

public class HomeHelper
{
    public static Dictionary<int, string> Days = new()
    {
        { 1, "Понедельник" },
        { 2, "Вторник" },
        { 3, "Среда" },
        { 4, "Четверг" },
        { 5, "Пятница" },
        { 6, "Суббота" },
        { 0, "Воскресенье" }
    };

    private Guid _id = FakeData.HardId;

    public Modal Modal = default!;
    public JournalFood SelectedFoodProduct;
    public int PortionSize;
    public DayOfWeek Today = DayOfWeek.Monday;
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
        User = UserRepository.GetById(_id);
        TodayMacronutrient = new Macronutrient();
    }

    public void ShowModalWindow(JournalFood journalFood)
    {
        SelectedFoodProduct = journalFood;
        Modal.Show();
    }

    public void ChangeFoodProductInfoAndSave(bool isEaten)
    {
        SelectedFoodProduct.PortionSize = PortionSize;
        SelectedFoodProduct.IsEaten = isEaten;
        FoodProductRepository.Update(SelectedFoodProduct);
    }

    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<JournalFood> fpsource)
    {
        return GetTotalMacronutrientsByDay(fpsource, Today);
    }

    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<JournalFood> fpsource, DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        foreach (var product in GetProductsByDay(fpsource, dayOfWeek))
        {
            totalMacronutrients += product.Macronutrient * product.PortionCoeff;
        }
        TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return TodayMacronutrient;
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
        return GetProductsByDay(fpSource, Today);
    }
    

    public IEnumerable<T> GetProductsByDay<T>(IEnumerable<T> fpSource, DayOfWeek dayOfWeek) where T: JournalFood
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
}