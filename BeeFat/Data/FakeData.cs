using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;

namespace BeeFat.Data;

public static class FakeData
{
    public static readonly Track FakeTrack = new Track("FakeTrack", "Some fake description");
    
    public static readonly ApplicationUser OlegRasin = new()
    {
        PersonName = new PersonName()
        {
            FirstName = "Олег",
            LastName = "Расин"
        },
        Track = FakeTrack,
        Age = 46,
        Height = 178,
        Weight = 80,
        RightCalories = 2000
    };

    public static readonly List<FoodProduct> FoodProducts = GetFoodProducts();

    private static List<FoodProduct> GetFoodProducts()
    {
        var eggMacronutrients = new Macronutrient(5, 10, 55, 100);
        var egg = new Food("Яйцо куриное", eggMacronutrients, 100);

        var water = new Food("Вода питьевая", new Macronutrient(), 100);

        var porridgeMacronutrient = new Macronutrient(10, 29, 3, 199);
        var porridge = new Food("Каша овсяная", porridgeMacronutrient, 101);

        var watermelonMacronutrient = new Macronutrient(0, 0, 30, 1000);
        var watermelon = new Food("Арбуз", watermelonMacronutrient, 99);

        var buckwheatMacronutrient = new Macronutrient(3, 1, 10, 200);
        var buckwheat = new Food("Греча", buckwheatMacronutrient, 300);

        return new List<FoodProduct>()
        {
        new FoodProductPiece(egg, 8, DayOfWeek.Monday, FakeTrack, false),
        new FoodProductGram(water, 3000, DayOfWeek.Monday, FakeTrack, false),
        new FoodProductGram(porridge, 400, DayOfWeek.Monday, FakeTrack, false),

        new FoodProductGram(water, 300, DayOfWeek.Tuesday, FakeTrack, false),
        new FoodProductGram(watermelon, 200, DayOfWeek.Tuesday, FakeTrack, false),
        new FoodProductGram(buckwheat, 500, DayOfWeek.Tuesday, FakeTrack, false),

        new FoodProductGram(buckwheat, 400, DayOfWeek.Wednesday, FakeTrack, false)
        };
    }

}