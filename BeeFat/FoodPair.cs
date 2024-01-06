using BeeFat.Domain.Infrastructure;

namespace BeeFat;

public class FoodPair
{
    public JournalFood JournalFood;
    public FoodProduct FoodProduct;

    public FoodPair(JournalFood journalFood, FoodProduct foodProduct)
    {
        JournalFood = journalFood;
        FoodProduct = foodProduct;
    }
}