using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Food : Entity
{
    [SetsRequiredMembers]
    public Food(string name, Macronutrient macronutrient, int weight)
    {
        Name = name;
        Macronutrient = macronutrient;
        Weight = weight;
    }
    [SetsRequiredMembers]
    public Food()
    {
        Name = "";
        Macronutrient = new Macronutrient();
        Weight = 0;
    }

    public required string Name { get; set; }
    
    public required Macronutrient Macronutrient { get; set; }
    public required int Weight { get; set; }

    public void EditFats(int fats) => Macronutrient.EditFats(fats);

    public void EditCarbohydrates(int carbohydrates) => Macronutrient.EditCarbohydrates(carbohydrates);

    public void EditProteins(int proteins) => Macronutrient.EditProteins(proteins);

}
