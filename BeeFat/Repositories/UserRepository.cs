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
        var context = _context;
        return context.BeeFatUsers.Where(selector);
    }

    public void RemoveUserJournalStory(Guid userId)
    {
        var context = _context;
        var user = userWithFoodProducts(context, userId);
        var journalFoods = user.Journal.FoodProducts.ToList();
        var trackFoodProducts = user.Track.FoodProducts.ToList();
        StaticBeeFat.MergeProductsFromTrackAndJournal(trackFoodProducts, journalFoods);
        foreach (var jf in journalFoods)
        {
            context.JournalFoods.Remove(jf);
        }

        context.SaveChanges();
    }

    private readonly Func<ApplicationDbContext, Guid, ApplicationUser> userWithFoodProducts = (db, id) => db.BeeFatUsers
        .Include(u => u.Journal)
        .ThenInclude(j => j.FoodProducts)
        .Include(u => u.Track)
        .ThenInclude(t => t.FoodProducts)
        .ThenInclude(fp => fp.Food)
        .First(u => u.Id == id.ToString());
}