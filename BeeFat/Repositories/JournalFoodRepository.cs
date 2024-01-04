using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class JournalFoodRepository : Repository<JournalFood>
{
    public JournalFoodRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(configuration, options)
    {
    }

    public override JournalFood GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override void Update(JournalFood entity)
    {
        using var context = _context;
        var foundFp = context.JournalFoods.FirstOrDefault(fp => fp.Id == entity.Id);
        foundFp.PortionSize = entity.PortionSize;
        foundFp.IsEaten = entity.IsEaten;
        context.SaveChanges();
    }

    public override IEnumerable<JournalFood> GetCollection(Func<JournalFood, bool> selector)
    {
        throw new NotImplementedException();
    }
}