using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Food : Entity
{
    [SetsRequiredMembers]
    public Food(string name, int fats, int carbohydrates, int proteins, int weight)
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
    public required int Weight { get; set; }
}
