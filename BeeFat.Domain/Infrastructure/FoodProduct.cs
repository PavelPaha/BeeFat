using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class FoodProduct : Entity
{
    public required int Count { get; set; }
    
    public Guid FoodId { get; set; }

    [ForeignKey("FoodId")]
    public required Food Food { get ; set; }
    
    public DayOfWeek DayOfWeek { get; set; } 

    public bool IsEaten { get; set; }
    
    [SetsRequiredMembers]
    public FoodProduct()
    {
    }

    [SetsRequiredMembers]
    public FoodProduct(int count, Guid foodId, DayOfWeek dayOfWeek, bool isEaten)
    {
        Count = count;
        FoodId = foodId;
        DayOfWeek = dayOfWeek;
        IsEaten = isEaten;
    }
}

