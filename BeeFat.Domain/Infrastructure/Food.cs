using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public abstract class Food : Entity 
{
    [SetsRequiredMembers]
    public Food(string name, Macronutrient macronutrient)
    {
        Name = name;
        Macronutrient = macronutrient;
    }
    [SetsRequiredMembers]
    public Food()
    {
        Name = "";
        Macronutrient = new Macronutrient();
        Weight = 0;
    }
    
    public ICollection<FoodProduct> FoodProducts { get; set; }

    public required string Name { get; set; }
    
    public required Macronutrient Macronutrient { get; set; }
    public required int Weight { get; set; }

    public void EditFats(int fats) => Macronutrient.EditFats(fats);

    public void EditCarbohydrates(int carbohydrates) => Macronutrient.EditCarbohydrates(carbohydrates);

    public void EditProteins(int proteins) => Macronutrient.EditProteins(proteins);

}
