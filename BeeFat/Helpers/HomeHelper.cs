using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;

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

    public HomeHelper(IBaseRepository repo)
    {
        Repo = repo;
        GetTotalMacronutrientsByDay(Today);
    }

    public IBaseRepository Repo { get; set; }

    public DayOfWeek Today = DayOfWeek.Monday;

    public Macronutrient TodayMacronutrient = new Macronutrient();

    public IEnumerable<FoodProduct> GetProductsByDay(DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        var todayDailyPlan = Repo.FoodProducts
            .Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
        foreach (var product in todayDailyPlan)
        {
            totalMacronutrients += product.Food.Macronutrient;
            yield return product;
        }
    }

    public Macronutrient GetTotalMacronutrientsByDay(DayOfWeek dayOfWeek)
    {
        var products = Repo.FoodProducts
            .Where(p => p.DayOfWeek.Equals(dayOfWeek)).ToList();
        
        var products1 = products
            .Select(p => new { Macronutrient = p.Food.Macronutrient, PortionSize = p.PortionCoeff});
        var totalMacronutrients = new Macronutrient();
        foreach (var product in products1)
        {
            totalMacronutrients += product.Macronutrient * product.PortionSize; 
        }
        TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return TodayMacronutrient;
    }

    public IEnumerable<IEnumerable<FoodProduct>> GetNextDaysProducts(DayOfWeek start)
    {
        using var context = new FakeApplicationDbContext();
        var todayNumber = (int)start;
        for (var dayNumber = todayNumber + 1; dayNumber <= 6 && dayNumber < todayNumber + 3; dayNumber++)
        {
            var products = context.FoodProducts.Where(p => (int)p.DayOfWeek == dayNumber);
            if (!products.Any()) break;
            yield return products;
        }
    }
}