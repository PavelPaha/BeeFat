using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class FoodRepository: Repository<Food>
{
    public FoodRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(configuration, options)
    {
    }

    public override Food GetById(Guid id)
    {
        var context = _context;
        return context.Foods.First(f => f.Id == id);
    }

    public override bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override void Update(Food entity)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Food> GetCollection(Func<Food, bool> selector)
    {
        var context = _context;
        return context.Foods.Where(selector);
    }
}