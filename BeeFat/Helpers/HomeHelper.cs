using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;
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

    public Modal Modal = default!;
    public FoodProduct SelectedFoodProduct;
    public int PortionSize;
    public DayOfWeek Today = DayOfWeek.Monday;
    public ApplicationUser User => Repo.User;
    public Track Track => User.Track;
    public Macronutrient TodayMacronutrient;
    public IBaseRepository Repo { get; }

    public void ShowModalWindow(FoodProduct foodProduct)
    {
        SelectedFoodProduct = foodProduct;
        Modal.Show();
    }

    public void SaveFoodProductWithChangedPortionSize()
    {
        Modal.Close(CloseReason.UserClosing);
        SelectedFoodProduct.PortionSize = PortionSize;
        SelectedFoodProduct.IsEaten = true;
        Repo.UpdatePortionSize(SelectedFoodProduct);
        GetTotalMacronutrientsByDay(Today);
    }

    public HomeHelper(IBaseRepository repo)
    {
        Repo = repo;
        TodayMacronutrient = new Macronutrient();
    }

    public Macronutrient GetTotalMacronutrientsByDay(DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        foreach (var product in Repo.GetProductsByDay(dayOfWeek))
        {
            totalMacronutrients += product.Food.Macronutrient * product.PortionCoeff;
        }
        TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return TodayMacronutrient;
    }

    public IEnumerable<IEnumerable<FoodProduct>> GetNextDaysFoodProducts(DayOfWeek start)
    {
        Repo.FetchUserInfo(); 
        var todayNumber = (int)start;
        for (var dayNumber = todayNumber + 1; dayNumber <= 6 && dayNumber < todayNumber + 3; dayNumber++)
        {
            var products = User.Track.FoodProducts.Where(p => (int)p.DayOfWeek == dayNumber).ToList();
            if (!products.Any()) break;
            yield return products;
        }
    }
}