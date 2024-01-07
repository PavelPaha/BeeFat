using System.Globalization;
using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using PersonName = BeeFat.Domain.Models.User.PersonName;

namespace BeeFat.Tests;

[TestFixture]
[Parallelizable]
public class ApplicationDbContextTests
{
    private IConfiguration _configuration;
    private DbContextOptions<ApplicationDbContext> _options;
    private Track _testTrack;

    private DbContextOptions<ApplicationDbContext> GetOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
            .Options;
    }


    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _configuration = configuration;
        _options = GetOptions();

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            _testTrack = new Track("DefaultTrack", "Some description");
            context.Tracks.Add(_testTrack);
            context.SaveChanges();
        }
    }


    [Test]
    public void User_ShouldAddUserToDatabaseAndRemove()
    {
        var user = new ApplicationUser(new PersonName
            {
                FirstName = "Кирилл",
                LastName = "Сарычев"
            },
            _testTrack.Id)
        {
            Age = 33,
            Height = 195,
            Weight = 140,
            JournalId = FakeData.HardJournalId
        };
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.BeeFatUsers.Add(user);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdUser = context.BeeFatUsers.FirstOrDefault(u => u.Id == user.Id);
            createdUser.Should().NotBeNull();
            createdUser.Should().Match<ApplicationUser>(user =>
                user.PersonName.FirstName == "Кирилл" &&
                user.PersonName.LastName == "Сарычев" &&
                user.Age == 33 &&
                user.Height == 195 &&
                user.Weight == 140
            );
        }

        // using (var context = new ApplicationDbContext(_options, _configuration))
        // {
        //     var userToDelete = context.BeeFatUsers.FirstOrDefault(u => u.Id == user.Id);
        //     userToDelete.Should().NotBeNull();
        //     context.BeeFatUsers.Remove(userToDelete);
        //     context.SaveChanges();
        //     var deletedUser = context.BeeFatUsers.FirstOrDefault(u => u.Id == user.Id);
        //     deletedUser.Should().BeNull();
        // }
    }


    [Explicit]
    [TestCase("b31c916a-87ac-4194-a385-010813b3d21a")]
    public void DeleteUser(string userId)
    {
        using var context = new ApplicationDbContext(_options, _configuration);
        var user = context.BeeFatUsers.First(u => u.Id == userId);
        context.BeeFatUsers.Remove(user);
        context.SaveChanges();
    }

    [Explicit]
    [Test]
    public void AddSomeFoodsToDatabase()
    {
        var eggMacronutrients = new Macronutrient(5, 10, 55, 100);
        var egg = new FoodPiece("Яйцо куриное", eggMacronutrients);

        var water = new FoodGram("Вода питьевая", new Macronutrient());

        var porridgeMacronutrient = new Macronutrient(10, 29, 3, 199);
        var porridge = new FoodGram("Каша овсяная", porridgeMacronutrient);

        var watermelonMacronutrient = new Macronutrient(0, 0, 30, 1000);
        var watermelon = new FoodGram("Арбуз", watermelonMacronutrient);

        var buckwheatMacronutrient = new Macronutrient(3, 1, 10, 200);
        var buckwheat = new FoodGram("Греча", buckwheatMacronutrient);
        var track = new Track("ЕЕЕЕЕЕЕЕ", "Some fake description");
        var foodProducts = new List<FoodProduct>()
        {
            new FoodProductPiece(egg, 8, DayOfWeek.Monday, track, false),
            new FoodProductGram(water, 3000, DayOfWeek.Monday, track, false),
            new FoodProductGram(porridge, 400, DayOfWeek.Monday, track, false),

            new FoodProductGram(water, 300, DayOfWeek.Tuesday, track, false),
            new FoodProductGram(watermelon, 200, DayOfWeek.Tuesday, track, false),
            new FoodProductGram(buckwheat, 500, DayOfWeek.Tuesday, track, false),

            new FoodProductGram(buckwheat, 400, DayOfWeek.Wednesday, track, false)
        };
        track.FoodProducts = foodProducts;

        var track1 = new Track("Track 1111", "Description for Track 1")
        {
            FoodProducts = foodProducts
        };
        var track2 = new Track("Кирилл Сарычев", "Description for Track 2");
        var track3 = new Track("Трэк Миши Иудинова", "Description for Track 3");
        var track4 = new Track("Трек Паши Васильева", "Description for Track 4");
        var track5 = new Track("Трек Димы Евтушенко", "Description for Track 5");
        var track6 = new Track("Какой-то ноунейм трек", "Description for Track 6");

        var fakeTracks = new List<Track>()
        {
            track1
        };

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            foreach (var t in fakeTracks)
            {
                context.Tracks.Add(t);
            }

            context.SaveChanges();
        }
    }

    [Explicit]
    [Test]
    public void AddJournalToUser()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            // var user = context.BeeFatUsers
            //     .Include(u => u.Track)
            //     .First(u => u.Id == FakeData.HardId);
            var foundJournal = new Journal(null);
            context.Journals.Add(foundJournal);
            context.SaveChanges();
        }
    }


    [Test]
    public void Food_ShouldAddFoodToDatabaseAndRemove()
    {
        var foodMacronutrient = new Macronutrient(10, 20, 1, 150);
        var food = new FoodGram("Apple", foodMacronutrient);
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.Foods.Add(food);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdFood = context.Foods.FirstOrDefault(f => f.Id == food.Id);
            createdFood.Should().NotBeNull();
            createdFood.Should().Match<Food>(f =>
                f.Name == "Apple" &&
                f.Macronutrient.Proteins == foodMacronutrient.Proteins &&
                f.Macronutrient.Fats == foodMacronutrient.Fats &&
                f.Macronutrient.Carbohydrates == foodMacronutrient.Carbohydrates &&
                f.Macronutrient.Calories == foodMacronutrient.Calories
            );
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var foodToDelete = context.Foods.FirstOrDefault(f => f.Id == food.Id);
            foodToDelete.Should().NotBeNull();
            context.Foods.Remove(foodToDelete);
            context.SaveChanges();
            var deletedFood = context.Foods.FirstOrDefault(f => f.Id == food.Id);
            deletedFood.Should().BeNull();
        }
    }


    [Explicit]
    [Test]
    public void DeleteAllEntriesFromFoods()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var foods = context.Foods;
            foreach (var food in foods)
            {
                context.Foods.Remove(food);
            }

            context.SaveChanges();
        }
    }


    [Test]
    public void FoodProductPiece_ShouldAddFoodProductPieceToDatabaseAndRemove()
    {
        var foodMacronutrient = new Macronutrient(7, 1, 7, 60);
        var food = new FoodPiece("Chicken egg", foodMacronutrient);
        var foodProduct = new FoodProductPiece(food, 5, DayOfWeek.Monday, _testTrack, false);
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.FoodProductsPieces.Add(foodProduct);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdFoodProduct = context.FoodProductsPieces
                .Include(fp => fp.Food)
                .FirstOrDefault(fp => fp.Id == foodProduct.Id);

            createdFoodProduct.Should().NotBeNull();
            createdFoodProduct.Should().Match<FoodProductPiece>(p =>
                p.DayOfWeek == DayOfWeek.Monday &&
                p.Pieces == 5 &&
                p.Food.Name == "Chicken egg" &&
                p.Food.Macronutrient.Proteins == foodMacronutrient.Proteins &&
                p.Food.Macronutrient.Fats == foodMacronutrient.Fats &&
                p.Food.Macronutrient.Carbohydrates == foodMacronutrient.Carbohydrates &&
                p.Food.Macronutrient.Calories == foodMacronutrient.Calories &&
                p.Food.Weight == 60
            );

            context.FoodProductsPieces.Remove(createdFoodProduct);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var deletedFoodProduct = context.FoodProductsPieces.FirstOrDefault(fp => fp.Id == foodProduct.Id);
            deletedFoodProduct.Should().BeNull();
        }
    }

    [Test]
    public void FoodProductGram_ShouldAddFoodProductGramToDatabaseAndRemove()
    {
        var foodMacronutrient = new Macronutrient(0, 0, 0, 1);
        var food = new FoodGram("Water", foodMacronutrient);
        var foodProduct = new FoodProductGram(food, 2000, DayOfWeek.Monday, _testTrack, false);
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.FoodProductsGrams.Add(foodProduct);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdFoodProduct = context.FoodProductsGrams
                .Include(fp => fp.Food)
                .FirstOrDefault(fp => fp.Id == foodProduct.Id);

            createdFoodProduct.Should().NotBeNull();
            createdFoodProduct.Should().Match<FoodProductGram>(p =>
                p.DayOfWeek == DayOfWeek.Monday &&
                p.Grams == 2000 &&
                p.Food.Name == "Water" &&
                p.Food.Macronutrient.Proteins == foodMacronutrient.Proteins &&
                p.Food.Macronutrient.Fats == foodMacronutrient.Fats &&
                p.Food.Macronutrient.Carbohydrates == foodMacronutrient.Carbohydrates &&
                p.Food.Macronutrient.Calories == foodMacronutrient.Calories &&
                p.Food.Weight == 100
            );

            context.FoodProductsGrams.Remove(createdFoodProduct);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var deletedFoodProduct = context.FoodProductsGrams.FirstOrDefault(fp => fp.Id == foodProduct.Id);
            deletedFoodProduct.Should().BeNull();
        }
    }


    [Explicit]
    [Test]
    public void DeleteAllEntriesFromFoodProducts()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var foodProductsPieces = context.FoodProductsPieces.ToList();
            context.FoodProductsPieces.RemoveRange(foodProductsPieces);

            var foodProductsGrams = context.FoodProductsGrams.ToList();
            context.FoodProductsGrams.RemoveRange(foodProductsGrams);
            context.SaveChanges();
        }
    }

    [Explicit]
    [Test]
    public void AddDataToFoodProductTable()
    {
        var track = new Track("DefaultTrack", "Some description");
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.Tracks.Add(track);
            context.SaveChanges();
        }

        var chickenBreastMacronutrients = new Macronutrient(2, 0, 26, 165);
        var chickenBreast = new FoodGram("Куриная грудка", chickenBreastMacronutrients);

        var riceMacronutrients = new Macronutrient(2, 29, 0, 130);
        var rice = new FoodGram("Рис", riceMacronutrients);

        var saladMacronutrients = new Macronutrient(1, 3, 0, 15);
        var salad = new FoodGram("Салат", saladMacronutrients);

        var salmonMacronutrients = new Macronutrient(13, 0, 20, 206);
        var salmon = new FoodGram("Лосось", salmonMacronutrients);

        // using (var context = new ApplicationDbContext(_options, _configuration))
        // {
        //     context.Foods.AddRange(new List<Food>() { chickenBreast, rice, salad, salmon });
        //     context.SaveChanges();
        // }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.FoodProductsGrams.Add(new FoodProductGram(chickenBreast, 200, DayOfWeek.Wednesday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(rice, 150, DayOfWeek.Wednesday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(salad, 1, DayOfWeek.Thursday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(chickenBreast, 150, DayOfWeek.Thursday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(rice, 100, DayOfWeek.Thursday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(salmon, 170, DayOfWeek.Sunday, track, false));
            context.SaveChanges();
        }
    }

    [Test]
    public void ShouldCorrectlyUserInformationUpdating()
    {
        var journal = new Journal(new List<JournalFood>());
        var user = new ApplicationUser(new PersonName
            {
                FirstName = "Кирилл",
                LastName = "Сарычев"
            },
            _testTrack.Id)
        {
            Journal = journal,
            JournalId = journal.Id,
            Age = 33,
            Height = 195,
            Weight = 140,
        };

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.BeeFatUsers.Add(user);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var foundUser = context.BeeFatUsers.First(u => u.Id == user.Id);
            foundUser.PersonName = new PersonName("Павел", "Васильев");
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var foundUser = context.BeeFatUsers.First(u => u.Id == user.Id);
            foundUser.PersonName.FirstName.Should().Be("Павел");
            foundUser.PersonName.LastName.Should().Be("Васильев");
        }
    }

    [Test]
    public void CorrectAddingSomeFoodProductsToOneFood()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var food = context.Foods.First();
            var track = context.Tracks.First();

            FoodProduct fp1;
            FoodProduct fp2;
            if (food is FoodGram foodGram)
            {
                fp1 = new FoodProductGram(foodGram, 200, DayOfWeek.Wednesday, track, false);
                fp2 = new FoodProductGram(foodGram, 400, DayOfWeek.Wednesday, track, true);
            }
            else
            {
                var foodPiece = food as FoodPiece;
                foodPiece.Should().NotBeNull();
                fp1 = new FoodProductPiece(foodPiece, 200, DayOfWeek.Wednesday, track, false);
                fp2 = new FoodProductPiece(foodPiece, 400, DayOfWeek.Wednesday, track, true);
            }

            context.FoodProducts.Add(fp1);
            context.FoodProducts.Add(fp2);
            context.SaveChanges();
        }
    }

    [Repeat(180)]
    [Explicit]
    [Test]
    public void DeleteLastFoodProductFromTrack()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var track = context.Tracks
                .Include(t => t.FoodProducts)
                .First(t => t.Id == Guid.Parse("0f912047-55a4-4283-8fb1-ba7c6f85165c"));
            track.FoodProducts.Remove(track.FoodProducts.Last());

            context.SaveChanges();
        }
    }

    [Explicit]
    [Test]
    public void AddFoodsToDb()
    {
        var pathToFoods = "StaticFiles/products.json";
        var json = File.ReadAllText(pathToFoods);

        var items = JsonConvert.DeserializeObject<List<Item>>(json);

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            foreach (var item in items)
            {
                Food food;
                var bguData = item.Bgu.Split(',');
                var proteins = (int)double.Parse(bguData[0], CultureInfo.InvariantCulture);
                var fats = (int)double.Parse(bguData[1], CultureInfo.InvariantCulture);
                var carbohydrates = (int)double.Parse(bguData[2], CultureInfo.InvariantCulture);
                var calories = (int)double.Parse(item.Kcal, CultureInfo.InvariantCulture);

                var macronutrient = new Macronutrient(proteins, fats, carbohydrates, calories);
                if (item.Name.ToLower().Contains("яйц")
                    || item.Name.ToLower().Contains("яиц")
                    || item.Name.ToLower().Contains("яич"))
                {
                    food = new FoodPiece(item.Name, macronutrient);
                }
                else
                {
                    food = new FoodGram(item.Name, macronutrient);
                }

                context.Foods.Add(food);
            }

            context.SaveChanges();
        }
    }

    [Explicit]
    [Test]
    public void AddTracksToDb()
    {
        var pathToFoods = "StaticFiles/products.json";
        var json = File.ReadAllText(pathToFoods);

        var items = JsonConvert.DeserializeObject<List<Item>>(json);
        var foodByIdFromJson = new Dictionary<int, Food>();
        foreach (var item in items)
        {
            Food food;
            var bguData = item.Bgu.Split(',');
            var proteins = (int)double.Parse(bguData[0], CultureInfo.InvariantCulture);
            var fats = (int)double.Parse(bguData[1], CultureInfo.InvariantCulture);
            var carbohydrates = (int)double.Parse(bguData[2], CultureInfo.InvariantCulture);
            var calories = (int)double.Parse(item.Kcal, CultureInfo.InvariantCulture);

            var macronutrient = new Macronutrient(proteins, fats, carbohydrates, calories);
            if (item.Name.ToLower().Contains("яйц")
                || item.Name.ToLower().Contains("яиц")
                || item.Name.ToLower().Contains("яич"))
            {
                food = new FoodPiece(item.Name, macronutrient);
            }
            else
            {
                food = new FoodGram(item.Name, macronutrient);
            }

            foodByIdFromJson[int.Parse(item.Id)] = food;
        }
        
        var pathToTrack = "StaticFiles/tracks.json";

        var jsonTrack = File.ReadAllText(pathToTrack);

        List<DayItem> days = JsonConvert.DeserializeObject<List<DayItem>>(jsonTrack);

        var fpsToDb = new List<FoodProduct>();
        var newTrack = new Track("Стандартный трек 2", "");
        newTrack.CaloriesByDay = 3800;
        
        
        for (int i = 0; i < 7; i++)
        {
            var dayNumber = (i + 1) % 7;
            var day = days[i%days.Count];
            var fpItems = new List<MealItem>(day.Завтрак);
            fpItems.AddRange(day.Обед);
            fpItems.AddRange(day.Перекус);
            fpItems.AddRange(day.Ужин);
            
            foreach (var mealItem in fpItems)
            {
                var food = foodByIdFromJson[mealItem.Продукт];
                FoodProduct fp;
                var curDay = Enum.Parse<DayOfWeek>(dayNumber.ToString());
                if (mealItem.Граммовка is null)
                {
                    if (food is FoodPiece piece)
                    {
                        fp = new FoodProductPiece(piece, (int)mealItem.Количество, curDay, newTrack, false);
                    }
                    else
                    {
                        var changedFood = new FoodPiece(food.Name, food.Macronutrient);
                        foodByIdFromJson[mealItem.Продукт] = changedFood;
                        fp = new FoodProductPiece(changedFood, (int)mealItem.Количество, curDay, newTrack, false);
                    }
                }
                else
                {
                    fp = new FoodProductGram((FoodGram)food, (int)mealItem.Граммовка, curDay, newTrack, false);
                }
                fpsToDb.Add(fp);
            }
        }
        
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.Tracks.Add(newTrack);
            foreach (var food in foodByIdFromJson.Keys)
            {
                context.Foods.Add(foodByIdFromJson[food]);
            }

            context.SaveChanges();

            foreach (var fp in fpsToDb)
            {
                context.FoodProducts.Add(fp);
            }

            context.SaveChanges();
        }
    }
}