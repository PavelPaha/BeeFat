using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class BeeFatRepository: IBaseRepository
{
    private ApplicationDbContext _db;

    public BeeFatRepository(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    {
        _db = new ApplicationDbContext(options, configuration);
    }
    
    public void Dispose()
    {
        _db.Dispose();
    }

    public ApplicationUser GetUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public void UpdatePortionSize(FoodProduct fp)
    {
        throw new NotImplementedException();
    }

    public ICollection<ApplicationUser> BeeFatUsers => _db.BeeFatUsers.ToList();
    
    public ICollection<Food> Foods => _db.Foods.Include(fp => fp.Macronutrient).ToList();
    public ICollection<FoodProduct> FoodProducts => _db.FoodProducts.Include(fp => fp.Food).ToList();
    public Track GetTrackByUser(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Track> GetTracksByCondition(Func<Track, bool> condition)
    {
        throw new NotImplementedException();
    }

    public void UpdateUserInfo(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Track GetTrackByUser(Guid userId)
    {
        return _db.BeeFatUsers
            .Include(applicationUser => applicationUser.Track)
            .First(user => user.Id == userId)
            .Track;
    }
}