using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class BeeFatRepository: IBaseRepository
{
    private ApplicationUser _user;
    public ApplicationUser User => _user;

    private DbContextOptions<ApplicationDbContext> _options;

    private IConfiguration _configuration;

    private Guid _id = Guid.Parse("a60197b1-90f9-4fb3-9c4b-1fff4ffe2b76");

    readonly Func<ApplicationDbContext, Guid, ApplicationUser> _getUserWithFoodProducts = (db, id) => 
        db.BeeFatUsers
        .Include(u => u.Track)
        .ThenInclude(t => t.FoodProducts)
        .ThenInclude(fp => fp.Food)
        .First(u => u.Id == id);
    
    public BeeFatRepository(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    {
        _options = options;
        _configuration = configuration;
        FetchUserInfo();
    }

    public void FetchUserInfo()
    {
        using (var db = new ApplicationDbContext(_options, _configuration))
        {
            _user = _getUserWithFoodProducts(db, _id);
        }
    }

    public ICollection<ApplicationUser> BeeFatUsers
    {
        get
        {
            using var db = new ApplicationDbContext(_options, _configuration);
            return db.BeeFatUsers.ToList();
        }
    }

    public ICollection<Food> Foods
    {
        get { 
            using var db = new ApplicationDbContext(_options, _configuration);
            return db.Foods.Include(fp => fp.Macronutrient).ToList(); 
        }
    }

    public ICollection<FoodProduct> FoodProducts
    {
        get
        {
            using var db = new ApplicationDbContext(_options, _configuration);
            return db.FoodProducts.Include(fp => fp.Food).ToList();
        }
    }

    public ApplicationUser GetUser(Guid id = new Guid())
    {
        throw new NotImplementedException();
    }

    public void UpdatePortionSize(FoodProduct fp)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Track> GetTracksByCondition(Func<Track, bool> condition)
    {
        using (var db = new ApplicationDbContext(_options, _configuration))
        {
            return db.Tracks.Where(condition).ToList();
        }
    }

    public IEnumerable<FoodProduct> GetFoodProductsByCondition(Func<FoodProduct, bool> condition)
    {
        throw new NotImplementedException();
    }

    public void UpdateUserInfo(ApplicationUser user)
    {
        using (var db = new ApplicationDbContext(_options, _configuration))
        {
            var foundUser = db.BeeFatUsers
                .First(u => u.Id == user.Id);
            foundUser.CloneFrom(user); 
            db.SaveChanges();
        }
    }

    public void DeleteFoodProductFromTrack(Track track, FoodProduct fp)
    {
        using (var db = new ApplicationDbContext(_options, _configuration)){
            db.Tracks.Find(track).FoodProducts.Remove(fp);
        }
    }

    public IEnumerable<FoodProduct> GetProductsByDay(DayOfWeek dayOfWeek)
    {
        FetchUserInfo();
        var totalMacronutrients = new Macronutrient();
        var todayDailyPlan = _user.Track.FoodProducts
            .Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
        foreach (var product in todayDailyPlan)
        {
            totalMacronutrients += product.Food.Macronutrient;
            yield return product;
        }
    }
}