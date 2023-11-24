namespace BeeFat.Domain.Infrastructure;

public class FoodProduct : ValueType<FoodProduct>
{
    public required string Count { get; set; }
    
    public bool IsEaten { get; set; }
}