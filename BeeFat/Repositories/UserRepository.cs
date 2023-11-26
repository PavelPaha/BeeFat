using BeeFat.Data;
using BeeFat.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class UserRepository : IBaseRepository<ApplicationUser>, IDisposable, IAsyncDisposable
{
    private ApplicationDbContext _db;

    public UserRepository(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    {
        _db = new ApplicationDbContext(options, configuration);
    }

    public bool Create(ApplicationUser entity)
    {
        var state = _db.BeeFatUsers.Add(entity).State;
        return state == EntityState.Added;
    }

    public ApplicationUser Get(Guid id)
    {
        return _db.BeeFatUsers
            .First(u => u.Id.Equals(id.ToString()));
    }

    public IEnumerable<ApplicationUser> Where(Func<ApplicationUser, bool> selector)
    {
        return _db.BeeFatUsers.Where(user => selector.Invoke(user));
    }

    public bool Delete(ApplicationUser entity)
    {
        var state = _db.BeeFatUsers.Remove(entity).State;
        return state == EntityState.Deleted;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _db.DisposeAsync();
    }
}