using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

public class FoodAdditionHelper
{
    public Modal SearchFoodWindow = default!;
    
    public Modal SetPortionSizeWindow = default!;
    public string SearchFood;
    public Food SelectedFood;
    
    public FoodRepository FoodRepository;
    public JournalFoodRepository JournalFoodRepository;

    public FoodAdditionHelper(FoodRepository foodRepository, JournalFoodRepository journalFoodRepository)
    {
        JournalFoodRepository = journalFoodRepository;
        FoodRepository = foodRepository;
        SearchFoodWindow = new Modal();
        SearchFoodWindow.Hide();
        SearchFoodWindow.Visibility = Visibility.Invisible;
        SearchFoodWindow.Visible = false;
    }

    public void ShowSetPortionSizeWindow(Food food)
    {
        SelectedFood = food;
        
    }

    public IEnumerable<Food> SearchFoods(string searchValue)
    {
        return FoodRepository.GetCollection(f => f.Name.ToLower().Contains(searchValue.ToLower()));
    }

    public void AddJournalFoodToJournal(JournalFood journalFood)
    {
        JournalFoodRepository.Add(journalFood);
    }

    public void RemoveJournalFoodFromJournal(JournalFood journalFood)
    {
        JournalFoodRepository.DeleteById(journalFood.Id);
    }
}