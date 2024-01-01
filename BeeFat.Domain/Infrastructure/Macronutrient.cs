using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Macronutrient : ValueType<Macronutrient>, IComparable
{
    public int Proteins { get; set; }
    public int Fats { get; set; }
    public int Carbohydrates { get; set; }
    public int Calories { get; set; }

    public Macronutrient() { 
        Proteins = 0;
        Fats = 0;
        Carbohydrates = 0;
        Calories = 0;
    }
    
    public Macronutrient(Macronutrient macronutrient) { 
        CopyMacronutrients(macronutrient);
    }

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
    
    public static Macronutrient operator *(Macronutrient macronutrient, double multiplier)
    {
        var multipliedProteins = (int)(macronutrient.Proteins * multiplier);
        var multipliedFats = (int)(macronutrient.Fats * multiplier);
        var multipliedCarbohydrates = (int)(macronutrient.Carbohydrates * multiplier);
        var multipliedCalories = (int)(macronutrient.Calories * multiplier);

        return new Macronutrient(multipliedProteins, multipliedFats, multipliedCarbohydrates, multipliedCalories);
    }
    
    public void CopyMacronutrients(Macronutrient other)
    {
        Proteins = other.Proteins;
        Fats = other.Fats;
        Carbohydrates = other.Carbohydrates;
        Calories = other.Calories;
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

    public int CompareTo(object? obj)
    {
        if (obj is Macronutrient other)
        {
            if (ReferenceEquals(this, other))
                return 0;

            var proteinsComparison = Proteins.CompareTo(other.Proteins);
            if (proteinsComparison != 0)
                return proteinsComparison;

            var fatsComparison = Fats.CompareTo(other.Fats);
            if (fatsComparison != 0)
                return fatsComparison;

            var carbohydratesComparison = Carbohydrates.CompareTo(other.Carbohydrates);
            if (carbohydratesComparison != 0)
                return carbohydratesComparison;

            return Calories.CompareTo(other.Calories);
        }

        throw new ArgumentException("Object is not macronutrients");
    }
}