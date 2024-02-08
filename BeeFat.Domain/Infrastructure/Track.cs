using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Track : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<FoodProduct> FoodProducts { get; set; }
    
    public int CaloriesByDay { get; set; }

    [SetsRequiredMembers]
    public Track(string name, string description)
    {
        Name = name;
        Description = description;
        FoodProducts = new List<FoodProduct>();
    }
}