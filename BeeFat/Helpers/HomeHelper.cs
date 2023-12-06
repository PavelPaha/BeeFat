using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Components.Pages;

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


    public IConfiguration Configuration { get; }

    public DbContextOptions<ApplicationDbContext> Options { get; }

    public HomeHelper(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    {
        Configuration = configuration;
        Options = options;
    }

    public IEnumerable<FoodProduct> GetProductsByDay(DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        using var context = new FakeApplicationDbContext();
        
        var todayDailyPlan = context.FoodProducts.Where(p => p.DayOfWeek.Equals(dayOfWeek));
        foreach (var product in todayDailyPlan)
        {
            totalMacronutrients += product.Food.Macronutrient;
            yield return product;
        }
    }

    public Macronutrient GetTotalMacronutrientsByDay(DayOfWeek dayOfWeek)
    {
        using var context = new FakeApplicationDbContext();
        var productMacronutrients = context.FoodProducts
            .Where(p => p.DayOfWeek.Equals(dayOfWeek))
            .Select(p => p.Food.Macronutrient);

        var totalMacronutrients = new Macronutrient();
        return productMacronutrients.Aggregate(totalMacronutrients, (current, macronutrient) => current + macronutrient);
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