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
    public FoodProduct SelectedFoodProduct;
    public int PortionSize;
    public DayOfWeek Today = DayOfWeek.Monday;
    public ApplicationUser User;
    public Macronutrient TodayMacronutrient;
    public UserRepository UserRepository;
    public JournalRepository JournalRepository;
    public FoodProductRepository FoodProductRepository;
    
    public HomeHelper(UserRepository userRepository, JournalRepository journalRepository, FoodProductRepository foodProductRepository)
    {
        FoodProductRepository = foodProductRepository;
        JournalRepository = journalRepository;
        UserRepository = userRepository;
        User = UserRepository.FetchUserInfo(_id);
        TodayMacronutrient = new Macronutrient();
    }

    public void ShowModalWindow(FoodProduct foodProduct)
    {
        SelectedFoodProduct = foodProduct;
        Modal.Show();
    }

    public void ChangeFoodProductInfoAndSave(bool isEaten)
    {
        SelectedFoodProduct.PortionSize = PortionSize;
        SelectedFoodProduct.IsEaten = isEaten;
        FoodProductRepository.Update(SelectedFoodProduct);
    }

    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<FoodProduct> fPsource, DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        foreach (var product in GetProductsByDay(fPsource, dayOfWeek))
        {
            totalMacronutrients += product.Food.Macronutrient * product.PortionCoeff;
        }
        TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return TodayMacronutrient;
    }

    public IEnumerable<IEnumerable<FoodProduct>> GetNextDaysFoodProducts(IEnumerable<FoodProduct> fPsource, DayOfWeek start)
    {
        var user = UserRepository.FetchUserInfo(_id);
        var todayNumber = (int)start;
        for (var dayNumber = todayNumber + 1; dayNumber <= 6 && dayNumber < todayNumber + 3; dayNumber++)
        {
            var products = fPsource.Where(p => (int)p.DayOfWeek == dayNumber).ToList();
            if (!products.Any()) break;
            yield return products;
        }
    }
    
    public IEnumerable<FoodProduct> GetProductsByDay(IEnumerable<FoodProduct> fPsource, DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        var todayDailyPlan = fPsource
            .Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
        foreach (var product in todayDailyPlan)
        {
            totalMacronutrients += product.Food.Macronutrient;
            yield return product;
        }
    }

    public ApplicationUser FetchUserInfo()
    {
        return UserRepository.FetchUserInfo(_id);
    }
}