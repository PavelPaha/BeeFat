using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public class Journal: Entity
{
    public ICollection<JournalFood> FoodProducts { get; set; }

    [SetsRequiredMembers]
    public Journal(ICollection<JournalFood> foodProducts)
    {
        FoodProducts = foodProducts;
    }

    [SetsRequiredMembers]
    protected Journal()
    {
    }
}