using BeeFat.Data;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public abstract class Repository<T>
{
    protected IConfiguration _configuration;
    protected DbContextOptions<ApplicationDbContext> _options;

    protected ApplicationDbContext _context => new(_options, _configuration);
    
    public Repository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
    {
        _configuration = configuration;
        _options = options;
    }

    public abstract T GetById(Guid id);

    public abstract bool DeleteById(Guid id);

    public abstract void Update(T entity);
    
    public abstract IEnumerable<T> GetCollection(Func<T, bool> selector);
}