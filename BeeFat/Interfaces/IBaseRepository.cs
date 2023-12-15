using BeeFat.Data;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Interfaces;

public interface IBaseRepository
{
    ApplicationUser User { get; }
    
    ICollection<Food> Foods { get; }
    
    ICollection<FoodProduct> FoodProducts { get; }

    void UpdatePortionSize(FoodProduct fp);
    
    IEnumerable<FoodProduct> GetProductsByDay(DayOfWeek dayOfWeek);

    IEnumerable<Track> GetTracksByCondition(Func<Track, bool> condition);
    
    IEnumerable<FoodProduct> GetFoodProductsByCondition(Func<FoodProduct, bool> condition);

    void UpdateUserInfo(ApplicationUser user); //обновляет в базе данных данные о пользователе
    
    void FetchUserInfo();

    void DeleteFoodProductFromTrack(Track track, FoodProduct fp);
}