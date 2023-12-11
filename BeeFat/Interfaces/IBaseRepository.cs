using BeeFat.Data;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Interfaces;

public interface IBaseRepository
{
    ICollection<ApplicationUser> BeeFatUsers { get; }
    
    ICollection<Food> Foods { get; }
    
    ICollection<FoodProduct> FoodProducts { get; }
    
    ApplicationUser GetUser(Guid id=new());

    void UpdatePortionSize(FoodProduct fp);

    public Track GetTrackByUser(ApplicationUser user);

    public IEnumerable<Track> GetTracksByCondition(Func<Track, bool> condition);

    public void UpdateUserInfo(ApplicationUser user); //обновляет в базе данных данные о пользователе
}