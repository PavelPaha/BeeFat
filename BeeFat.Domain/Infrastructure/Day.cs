using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Day : Entity
{
    [SetsRequiredMembers]
    protected Day(DaysOfWeek weekDay, List<Food> foodProducts)
    {
        WeekDay = weekDay;
        FoodProducts = foodProducts;
    }

    public DaysOfWeek WeekDay { get; set; }
    
    public List<Food> FoodProducts { get; }

    public int GetTotalFats 
        => FoodProducts.Select(food => food.Fats).Sum();
    
    public int GetTotalCarbohydrates 
        => FoodProducts.Select(food => food.Carbohydrates).Sum();
    
    public int GetTotalProteins 
        => FoodProducts.Select(food => food.Proteins).Sum();
    

    public void AddFood(Food food)
    {
        FoodProducts.Add(food);
    }

    public void RemoveFoodByIndex(int index)
    {
        FoodProducts.RemoveAt(index);
    }
}

public enum DaysOfWeek
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7
}
