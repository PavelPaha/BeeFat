namespace BeeFat.Domain.Infrastructure;

public class Food : ValueType<Food>
{
    public required string Name { get; set; }
    
    public required int Fats { get; set; }

    public required int Carbohydrates { get; set; }

    public required int Proteins { get; set; }
    
    public required int Weight { get; set; }
}