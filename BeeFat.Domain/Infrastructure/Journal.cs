using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BeeFat.Data;

namespace BeeFat.Domain.Infrastructure;

public class Journal: Entity
{
    public ICollection<FoodProduct> FoodProducts { get; set; }

    [SetsRequiredMembers]
    public Journal(ICollection<FoodProduct> foodProducts)
    {
        FoodProducts = foodProducts;
    }

    [SetsRequiredMembers]
    protected Journal()
    {
    }
}