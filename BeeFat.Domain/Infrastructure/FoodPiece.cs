using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class FoodPiece: Food
{
    [SetsRequiredMembers]
    public FoodPiece(string name, Macronutrient macronutrient) : base(name, macronutrient)
    {
    }

    [SetsRequiredMembers]
    public FoodPiece()
    {
    }
}