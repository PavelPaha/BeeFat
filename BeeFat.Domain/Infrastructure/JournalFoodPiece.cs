using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class JournalFoodPiece: JournalFood
{
    [SetsRequiredMembers]
    public JournalFoodPiece()
    {
    }

    [SetsRequiredMembers]
    public JournalFoodPiece(string name, Macronutrient macronutrient,int pieces, DayOfWeek dayOfWeek, Journal journal, int portionSize, bool isEaten)
        : base(name, journal, dayOfWeek, macronutrient, portionSize, isEaten)
    {
        Pieces = pieces;
    }

    public override double PortionCoeff => PortionSize;

    public required int Pieces { get; set; }
}