using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class FoodGram: Food
{
    [SetsRequiredMembers]
    public FoodGram(string name, Macronutrient macronutrient) : base(name, macronutrient)
    {
    }

    [SetsRequiredMembers]
    public FoodGram()
    {
    }
}