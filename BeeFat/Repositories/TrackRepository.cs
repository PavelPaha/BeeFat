using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class TrackRepository: Repository<Track>
{
    public TrackRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(configuration, options)
    {
    }

    public override Track GetById(Guid id)
    {
        using (var context = _context)
        {
            return context
                .Tracks
                .Include(t => t.FoodProducts)
                .ThenInclude(fp => fp.Food)
                .First(u => u.Id == id);
        }
    }
    

    public override bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override void Update(Track entity)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Track> GetCollection(Func<Track, bool> selector)
    {
        using (var db = _context)
        {
            return db.Tracks.Where(selector).ToList();
        }
    }
    
    public void DeleteFoodProductFromTrack(Track track, FoodProduct fp)
    {
        using (var db = _context){
            db.Tracks.Find(track).FoodProducts.Remove(fp);
        }
    }
}