using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public abstract class FoodProduct : Entity
{
    public required string Name { get; set; }
    
    public Guid FoodId { get; set; }

    [ForeignKey("FoodId")]
    public required Food Food { get ; set; }
    
    public DayOfWeek DayOfWeek { get; set; } 

    public bool IsEaten { get; set; }
    
    public abstract int PortionSize { get; }
    
    [SetsRequiredMembers]
    protected FoodProduct()
    {
    }

    [SetsRequiredMembers]
    protected FoodProduct(Food food, DayOfWeek dayOfWeek, bool isEaten)
    {
        Food = food;
        FoodId = Food.Id;
        DayOfWeek = dayOfWeek;
        IsEaten = isEaten;
    }

    public abstract void ChangePortionSize(int newPortionSize);

    protected virtual void EnsurePortionSize(int portionSize)
    {
        if (portionSize < 0)
            throw new ArgumentException("Размер порции должен быть неотрицательным");
    }
}

