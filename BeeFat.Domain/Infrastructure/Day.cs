using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Day : Entity
{
    [SetsRequiredMembers]
    protected Day(DaysOfWeek weekDay, List<FoodProduct> foodProducts)
    {
        WeekDay = weekDay;
        FoodProducts = foodProducts;
    }
    
    [SetsRequiredMembers]
    public Day()
    {
    }
    
    public DaysOfWeek WeekDay { get; set; }
    
    public List<FoodProduct> FoodProducts { get; }
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
