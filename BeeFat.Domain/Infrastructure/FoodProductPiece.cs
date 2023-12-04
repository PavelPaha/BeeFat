using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class FoodProductPiece : FoodProduct
{
    public int Pieces { get; set; }
    
    [SetsRequiredMembers]
    public FoodProductPiece()
    {
    }

    [SetsRequiredMembers]
    public FoodProductPiece(Food food, int piece, DayOfWeek dayOfWeek, bool isEaten) : base(food, dayOfWeek, isEaten)
    {
        Pieces = piece;
        Name = Food.Name;
    }

    public override void ChangePortionSize(int newPortionSize)
    {
        EnsurePortionSize(newPortionSize);
        Pieces = newPortionSize;
    }

    protected override void EnsurePortionSize(int grams)
    {
        if (grams <= 0)
            throw new ArgumentException($"Количество штук в порции \"{Name}\" должно быть > 0");
    }
}