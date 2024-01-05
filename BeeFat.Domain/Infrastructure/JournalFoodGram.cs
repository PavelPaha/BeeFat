using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class JournalFoodGram : JournalFood
{
    [SetsRequiredMembers]
    public JournalFoodGram()
    {
    }

    [SetsRequiredMembers]
    public JournalFoodGram(string name, Macronutrient macronutrient, DayOfWeek dayOfWeek, Journal journal, int portionSize, bool isEaten)
        : base(name, journal, dayOfWeek, macronutrient, portionSize, isEaten)
    {
    }

    public required int Grams { get; set; }
}