using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace BeeFat.Helpers;

public class TrackViewerHelper
{
    public TrackPickHelper TrackPickHelper;
    public TrackRepository TrackRepository;
    public int PortionSize = 0;
    public Modal ConfirmModal = default!;
    public Modal AddProductModal = default!;
    public JournalFood SelectedFoodProduct = null;
    public Macronutrient TodayMacronutrient;
    // private IEnumerable<FoodProduct> AvailableFoodProducts;
    private string searchValue;
    
    public TrackViewerHelper(TrackPickHelper trackPickHelper, TrackRepository trackRepository)
    {
        TrackPickHelper = trackPickHelper;
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
        return GetProductsByDay(fpSource, StaticBeeFat.Today);
    }

    public IEnumerable<FoodProduct> GetProductsByDay(IEnumerable<FoodProduct> fpSource, DayOfWeek dayOfWeek)
    {
        return fpSource.Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
    }

    public void ChangeTrack(string trackName)
    {
        var track = TrackRepository.GetFirstByCondition(track => track.Name == trackName);
        TrackPickHelper.ChangeSelectedTrack(track);
        TrackPickHelper.Save();
    }
}