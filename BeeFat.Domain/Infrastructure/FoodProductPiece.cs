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
    public FoodProductPiece(FoodPiece food, int pieces, DayOfWeek dayOfWeek, Track track, bool isEaten)
        : base(food, dayOfWeek, track, isEaten)
    {
        // Track = track;
        Pieces = pieces;
        Name = food.Name;
    }

    public override int PortionSize
    {
        get => Pieces;
        set => Pieces = value;
    }
}