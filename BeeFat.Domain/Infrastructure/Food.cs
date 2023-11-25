using Microsoft.AspNetCore.Identity;

namespace BeeFat.Domain.Infrastructure;

public class Food : BaseEntity
{
    public required string Name { get; set; }
    public required int Fats { get; set; }
    public required int Carbohydrates { get; set; }
    public required int Proteins { get; set; }

    public required int Weight { get; set; }

    public Food()
    {
    }
}
