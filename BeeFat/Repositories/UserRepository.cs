using BeeFat.Components;
using BeeFat.Data;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class UserRepository: Repository<ApplicationUser>
{
    public UserRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options): base(configuration, options)
    {
        
    }

    public override ApplicationUser GetById(Guid id)
    {
        ApplicationUser user = null;
        using (var context = _context)
        {
            user = context.BeeFatUsers.FirstOrDefault(u => u.Id == id);
            if (user is null)
            {
                throw new Exception($"Пользователя с id = {id} не существует");
            }
        }
        return user;
    }

    public override bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
    
    public ApplicationUser FetchUserInfo(Guid id)
    {
        using (var db = new ApplicationDbContext(_options, _configuration))
        {
            return _getUserWithFoodProducts(db, id);
        }
    }
    
        
    readonly Func<ApplicationDbContext, Guid, ApplicationUser> _getUserWithFoodProducts = (db, id) => 
        db.BeeFatUsers
            .Include(u => u.Track)
            .ThenInclude(t => t.FoodProducts)
            .ThenInclude(fp => fp.Food)
            .First(u => u.Id == id);
}