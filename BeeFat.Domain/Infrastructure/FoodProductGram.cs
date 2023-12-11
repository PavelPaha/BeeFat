using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class FoodProductGram : FoodProduct
{
    public required int Grams { get; set; }

    [SetsRequiredMembers]
    protected FoodProductGram()
    {
    }

    [SetsRequiredMembers]
    public FoodProductGram(Food food, int grams, DayOfWeek dayOfWeek, Track track, bool isEaten)
        : this(food, grams, dayOfWeek, track.Id, isEaten)
    {
        Track = track;
    }

    [SetsRequiredMembers]
    public FoodProductGram(Food food, int grams, DayOfWeek dayOfWeek, Guid trackId, bool isEaten)
        : base(food, dayOfWeek, trackId, isEaten)
    {
        Grams = grams;
        Name = Food.Name;
    }


    public override int PortionSize
    {
        get => Grams;
        set => Grams = value;
    }

    public override void ChangePortionSize(int newPortionSize)
    {
        EnsurePortionSize(newPortionSize);
        Grams = newPortionSize;
    }

    protected override void EnsurePortionSize(int grams)
    {
        if (grams <= 0)
            throw new ArgumentException($"Количество грамм в порции  \"{Name}\" должно быть > 0");
    }
}