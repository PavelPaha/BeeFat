using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class JournalFoodGram : JournalFood
{
    [SetsRequiredMembers]
    public JournalFoodGram()
    {
    }

    [SetsRequiredMembers]
    public JournalFoodGram(string name, Macronutrient macronutrient,int grams, DayOfWeek dayOfWeek, Journal journal, int portionSize, bool isEaten)
        : base(name, journal, dayOfWeek, macronutrient, portionSize, isEaten)
    {
        Grams = grams;
    }

    public required int Grams { get; set; }
}