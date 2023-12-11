using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BeeFat.Data;

namespace BeeFat.Domain.Infrastructure;

public class Journal: Entity
{
    [SetsRequiredMembers]
    protected Journal(Guid userId, ApplicationUser user)
    {
        UserId = userId;
        User = user;
    }

    [SetsRequiredMembers]
    protected Journal()
    {
    }

    public IEnumerable<FoodProduct> FoodProducts
    {
        get
        {
            return User.Track.FoodProducts.Where(fp => fp.IsEaten);
        }
    }

    public Guid UserId { get; set; }
    
    [ForeignKey("UserId")]
    public required ApplicationUser User { get; set; }
}