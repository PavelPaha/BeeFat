using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Macronutrient : ValueType<Macronutrient>
{
    public int Proteins { get; set; }
    public int Fats { get; set; }
    public int Carbohydrates { get; set; }
    public int Calories { get; set; }

    public Macronutrient() { }

    public Macronutrient(int proteins, int fats, int carbohydrates, int calories)
    {
        Proteins = proteins;
        Fats = fats;
        Carbohydrates = carbohydrates;
        Calories = calories;
    }

    public static Macronutrient operator +(Macronutrient macronutrients1, Macronutrient macronutrients2)
    {
        var summedProteins = macronutrients1.Proteins + macronutrients2.Proteins;
        var summedFats = macronutrients1.Fats + macronutrients2.Fats;
        var summedCarbohydrates = macronutrients1.Carbohydrates + macronutrients2.Carbohydrates;
        var summedCalories = macronutrients1.Calories + macronutrients2.Calories;

        return new Macronutrient(summedProteins, summedFats, summedCarbohydrates, summedCalories);
    }
    
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