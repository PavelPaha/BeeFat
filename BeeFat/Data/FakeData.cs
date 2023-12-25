using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;

namespace BeeFat.Data;

public static class FakeData
{
    public static readonly List<FoodProduct> FoodProducts;

    public static readonly Track FakeTrack;

    public static readonly ApplicationUser OlegRasin;

    public static readonly IEnumerable<Track> FakeTracks;
    
    public static Guid HardId = Guid.Parse("cfb11027-8477-49eb-bc3d-83aa08ae5bbc");

    public static Guid HardJournalId = Guid.Parse("2d998d88-536f-4106-bb21-09f86431f58d");
    
    static FakeData()
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
        FakeTrack = new Track("FakeTrack", "Some fake description");
        FoodProducts = new List<FoodProduct>()
        {
            new FoodProductPiece(egg, 8, DayOfWeek.Monday, FakeTrack, false),
            new FoodProductGram(water, 3000, DayOfWeek.Monday, FakeTrack, false),
            new FoodProductGram(porridge, 400, DayOfWeek.Monday, FakeTrack, false),

            new FoodProductGram(water, 300, DayOfWeek.Tuesday, FakeTrack, false),
            new FoodProductGram(watermelon, 200, DayOfWeek.Tuesday, FakeTrack, false),
            new FoodProductGram(buckwheat, 500, DayOfWeek.Tuesday, FakeTrack, false),

            new FoodProductGram(buckwheat, 400, DayOfWeek.Wednesday, FakeTrack, false)
        };
        FakeTrack.FoodProducts = FoodProducts;
        
        OlegRasin  = new ApplicationUser(new PersonName()
        {
            FirstName = "Олег",
            LastName = "Расин"
        }, FakeTrack)
        {
            Age = 46,
            Height = 178,
            Weight = 80,
            RightCalories = 2000,
            Id = default,
        };
        
        var track1 = new Track("Track 1", "Description for Track 1");
        var track2 = new Track("Кирилл Сарычев", "Description for Track 2");
        var track3 = new Track("Трэк Миши Иудинова", "Description for Track 3");
        var track4 = new Track("Трек Паши Васильева", "Description for Track 4");
        var track5 = new Track("Трек Димы Евтушенко", "Description for Track 5");
        var track6 = new Track("Какой-то ноунейм трек", "Description for Track 6");

        FakeTracks = new List<Track>()
        {
            FakeTrack, track1, track2, track3, track4, track5, track6
        };
    }
}