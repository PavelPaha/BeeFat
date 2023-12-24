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
    public FoodProduct SelectedFoodProduct = null;
    private IEnumerable<FoodProduct> AvailableFoodProducts;
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
}