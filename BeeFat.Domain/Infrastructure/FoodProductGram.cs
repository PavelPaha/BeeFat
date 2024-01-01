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
        : base(food, dayOfWeek, track, isEaten)
    {
        // Track = track;
        Grams = grams;
        Name = food.Name;
    }


    public override int PortionSize
    {
        get => Grams;
        set => Grams = value;
    }
}