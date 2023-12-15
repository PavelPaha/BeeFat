using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;

namespace BeeFat.Repositories;

public class FakeBeeFatRepository
{
    public ICollection<ApplicationUser> BeeFatUsers { get; }
    public ICollection<Food> Foods { get; }
    public ICollection<FoodProduct> FoodProducts { get; }
    
    private Track FakeTrack;
    private ApplicationUser FakeUser;
    private IEnumerable<Track> FakeTracks;

    public FakeBeeFatRepository()
    {
        FakeUser = FakeData.OlegRasin;
        FakeTrack = FakeUser.Track;
        FoodProducts = FakeData.FoodProducts;
        FakeTracks = FakeData.FakeTracks;
    }

    public ApplicationUser GetUser(Guid id=new())
    {
        return FakeUser;
    }

    public Track GetTrackByUser(ApplicationUser user)
    {
        return FakeTrack;
    }

    public IEnumerable<Track> GetTracksByCondition(Func<Track, bool> condition)
    {
        return FakeTracks.Where(condition).ToList();
    }

    public IEnumerable<FoodProduct> GetFoodProductsByCondition(Func<FoodProduct, bool> condition)
    {
        throw new NotImplementedException();
    }

    public void UpdateUserInfo(ApplicationUser user)
    {
        FakeUser.CloneFrom(user);
    }

    public void DeleteFoodProductFromTrack(Track track, FoodProduct foodProduct)
    {
        var foundFoodProduct = FakeTrack.FoodProducts.First(fp => fp.Id == foodProduct.Id);
        FakeTrack.FoodProducts.Remove(foundFoodProduct);
    }

    public void UpdatePortionSize(FoodProduct updatedFp)
    {
        var fpList = (List<FoodProduct>)FoodProducts;
        var foodProduct = FoodProducts.FirstOrDefault(fp => fp.Id == updatedFp.Id);

        var indexOldFp = fpList.IndexOf(foodProduct);
        fpList[indexOldFp] = updatedFp;
    }
    
    public IEnumerable<FoodProduct> GetProductsByDay(DayOfWeek dayOfWeek)
    {
        var totalMacronutrients = new Macronutrient();
        var todayDailyPlan = FakeTrack.FoodProducts
            .Where(fp => fp.DayOfWeek.Equals(dayOfWeek));
        foreach (var product in todayDailyPlan)
        {
            totalMacronutrients += product.Food.Macronutrient;
            yield return product;
        }
    }
}