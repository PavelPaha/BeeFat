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
            var track = new Track("FakeTrack", "Some fake description");
            var eggMacronutrients = new Macronutrient(5, 10, 55, 100);
            var egg = new Food("Яйцо куриное", eggMacronutrients,  100);
            
            var water = new Food("Вода питьевая", new Macronutrient(), 100);

            var porridgeMacronutrient = new Macronutrient(10, 29, 3, 199);
            var porridge = new Food("Каша овсяная",  porridgeMacronutrient, 101);

            var watermelonMacronutrient = new Macronutrient(0, 0, 30, 1000);
            var watermelon = new Food("Арбуз", watermelonMacronutrient, 99);

            var buckwheatMacronutrient = new Macronutrient(3, 1, 10, 200);
            var buckwheat = new Food("Греча",  buckwheatMacronutrient, 300);
            
            FoodProducts = new List<FoodProduct>()
            {
                new FoodProductPiece(egg, 8, DayOfWeek.Monday, track, false),
                new FoodProductGram(water, 3000, DayOfWeek.Monday, track, false),
                new FoodProductGram(porridge, 400, DayOfWeek.Monday, track, false),
                
                
                new FoodProductGram(water, 300, DayOfWeek.Tuesday, track, false),
                new FoodProductGram(watermelon, 200, DayOfWeek.Tuesday, track, false),
                new FoodProductGram(buckwheat, 500, DayOfWeek.Tuesday, track, false),
                
                new FoodProductGram(buckwheat, 400, DayOfWeek.Wednesday, track, false)
            };
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}
