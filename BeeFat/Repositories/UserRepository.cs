using BeeFat.Data;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class UserRepository : Repository<ApplicationUser>
{
    public UserRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(
        configuration, options)
    {
    }

    public override ApplicationUser GetById(Guid id)
    {
        using var context = _context;
        // return null;
        return userWithFoodProducts(context, id);
    }

    public override bool DeleteById(Guid id)
    {
        throw new NotSupportedException();
    }

    public override void Update(ApplicationUser user)
    {
        using (var db = _context)
        {
            var foundUser = db.BeeFatUsers
                .First(u => u.Id == user.Id);
            foundUser.CloneFrom(user);
            db.SaveChanges();
        }
    }

    public override IEnumerable<ApplicationUser> GetCollection(Func<ApplicationUser, bool> selector)
    {
        throw new NotSupportedException();
    }

    private readonly Func<ApplicationDbContext, Guid, ApplicationUser> userWithFoodProducts = (db, id) => db.BeeFatUsers
        .Include(u => u.Journal)
        .ThenInclude(j => j.FoodProducts)
        // .ThenInclude(fp => fp.Journal)
        .Include(u => u.Track)
        .ThenInclude(t => t.FoodProducts)
        .ThenInclude(fp => fp.Food)
        .First(u => u.Id == id.ToString());
}