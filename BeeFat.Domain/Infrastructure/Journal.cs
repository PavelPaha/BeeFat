using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BeeFat.Data;

namespace BeeFat.Domain.Infrastructure;

public class Journal: Entity
{
    public ICollection<FoodProduct> FoodProducts { get; set; }
    
    [SetsRequiredMembers]
    protected Journal(Guid userId, ApplicationUser user)
    {
        UserId = userId;
        User = user;
        FoodProducts = User.Track.FoodProducts;
    }

    [SetsRequiredMembers]
    protected Journal()
    {
    }

    public Guid UserId { get; set; }
    
    [ForeignKey("UserId")]
    public required ApplicationUser User { get; set; }
}