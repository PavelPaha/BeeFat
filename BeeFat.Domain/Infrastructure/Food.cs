using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Food : Entity
{
    [SetsRequiredMembers]
    public Food(string name, int proteins, int fats, int carbohydrates, int calories, int weight)
    {
        Name = name;
        Fats = fats;
        Carbohydrates = carbohydrates;
        Proteins = proteins;
        Weight = weight;
    }

    public required string Name { get; set; }
    public required int Fats { get; set; }
    public required int Carbohydrates { get; set; }
    public required int Proteins { get; set; }
    
    public required int Calories { get; set; }
    public required int Weight { get; set; }

    public void EditFats(int fats)
    {
        EnsureMacronutrientValue(fats);
        Fats = fats;
    }
    
    public void EditCarbohydrates(int carbohydrates)
    {
        EnsureMacronutrientValue(carbohydrates);
        Carbohydrates = carbohydrates;
    }
    
    public void EditProteins(int proteins)
    {
        EnsureMacronutrientValue(proteins);
        Proteins = proteins;
    }

    private static void EnsureMacronutrientValue(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Значение макроэлемента должно быть больше или равно нулю.");
        }
    }
}
