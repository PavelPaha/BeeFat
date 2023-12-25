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
    public FoodProductPiece(Food food, int pieces, DayOfWeek dayOfWeek, Track track, bool isEaten)
        : this(food, pieces, dayOfWeek, track.Id, isEaten)
    {
        Track = track;
    }

    [SetsRequiredMembers]
    public FoodProductPiece(Food food, int pieces, DayOfWeek dayOfWeek, Guid trackId, bool isEaten)
        : base(food, dayOfWeek, trackId, isEaten)
    {
        Pieces = pieces;
        Name = food.Name;
    }


    public override int PortionSize
    {
        get => Pieces;
        set => Pieces = value;
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