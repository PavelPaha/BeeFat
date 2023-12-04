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
    public FoodProductGram(Food food, int grams, DayOfWeek dayOfWeek, bool isEaten)
        : base(food, dayOfWeek, isEaten)
    {
        Grams = grams;
        Name = Food.Name;
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