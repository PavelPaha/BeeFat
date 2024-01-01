using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace BeeFat.Helpers;

public class TrackEditorHelper
{
    public TrackRepository TrackRepository;
    public int PortionSize = 0;
    public Modal ConfirmModal = default!;
    public Modal AddProductModal = default!;
    public JournalFood SelectedFoodProduct = null;
    public Macronutrient TodayMacronutrient;
    public DayOfWeek Today = DayOfWeek.Monday;
    // private IEnumerable<FoodProduct> AvailableFoodProducts;
    private string searchValue;
    
    public TrackEditorHelper(TrackRepository trackRepository)
    {
        TrackRepository = trackRepository;
    }
    
    public void ChangePortionSize()
    {
        SelectedFoodProduct.PortionSize = PortionSize;
        // Repo.UpdatePortionSize(selectedFoodProduct);
        ConfirmModal.Close(CloseReason.UserClosing);
    }

    private void OnSearchValueChanged(ChangeEventArgs e)
    {
        searchValue = e.Value.ToString();
        // AvailableFoodProducts = HomeHelper.GetFoodProductsByCondition(fp => fp.Name.ToLower().Contains(searchValue.ToLower()));
    }
    
    public Macronutrient GetTotalMacronutrientsByDay(IEnumerable<FoodProduct> fpsource, DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        foreach (var product in GetProductsByDay(fpsource, dayOfWeek))
        {
            totalMacronutrients += product.Food.Macronutrient * product.PortionCoeff;
        }
        TodayMacronutrient.CopyMacronutrients(totalMacronutrients);
        return TodayMacronutrient;
    }

    public IEnumerable<FoodProduct> GetProductsByDay(IEnumerable<FoodProduct> fpSource)
    {
        return GetProductsByDay(fpSource, Today);
    }

    public IEnumerable<FoodProduct> GetProductsByDay(IEnumerable<FoodProduct> fpSource, DayOfWeek dayOfWeek)
    {
        return fpSource.Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
    }
}