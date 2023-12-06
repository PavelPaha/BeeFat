using System.Data.Entity;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Data
{
    public class FakeApplicationDbContext: IDisposable
    {
        public IEnumerable<ApplicationUser> BeeFatUsers { get; set; }

        public List<FoodProduct> FoodProducts { get; set; }
        public FakeApplicationDbContext()
        {
            var egg = new Food("Яйцо куриное", 15, 5, 10, 55, 100);
            var water = new Food("Вода питьевая", 0, 0, 0, 100, 100);
            var porridge = new Food("Каша овсяная", 10, 29, 3, 199, 101);
            var watermelon = new Food("Арбуз", 0, 0, 30, 1000, 99);
            var buckwheat = new Food("Греча", 3, 1, 10, 200, 98);
            
            FoodProducts = new List<FoodProduct>()
            {
                new FoodProductPiece(egg, 8, DayOfWeek.Monday, false),
                new FoodProductGram(water, 3000, DayOfWeek.Monday, false),
                new FoodProductGram(porridge, 400, DayOfWeek.Monday, false),
                
                
                new FoodProductGram(water, 300, DayOfWeek.Tuesday, false),
                new FoodProductGram(watermelon, 200, DayOfWeek.Tuesday, false),
                new FoodProductGram(buckwheat, 500, DayOfWeek.Tuesday, false),
                
                new FoodProductGram(buckwheat, 400, DayOfWeek.Wednesday, false)
            };
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}
