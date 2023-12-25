using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class FoodProductRepository: Repository<FoodProduct>
{
    public FoodProductRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(configuration, options)
    {
    }

    public override FoodProduct GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override void Update(FoodProduct entity)
    {
        using (var context = _context)
        {
            var foundFp = context.FoodProducts.First(fp => fp.Id == entity.Id);
            foundFp.PortionSize = entity.PortionSize;
            foundFp.IsEaten = entity.IsEaten;
            context.SaveChanges();
        }
    }

    public override IEnumerable<FoodProduct> GetCollection(Func<FoodProduct, bool> selector)
    {
        throw new NotImplementedException();
    }
}