using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            RightCalories = 3000
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

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var userToDelete = context.BeeFatUsers.FirstOrDefault(u => u.Id == user.Id);
            userToDelete.Should().NotBeNull();
            context.BeeFatUsers.Remove(userToDelete);
            context.SaveChanges();
            var deletedUser = context.BeeFatUsers.FirstOrDefault(u => u.Id == user.Id);
            deletedUser.Should().BeNull();
        }
    }


    // [Explicit]
    // [Test]
    // public void DeleteAllEntriesFromUsers()
    // {
    //     using (var context = new ApplicationDbContext(_options, _configuration))
    //     {
    //         var users = context.BeeFatUsers.ToList();
    //         context.BeeFatUsers.RemoveRange(users);
    //         context.SaveChanges();
    //     }
    // }


    [Test]
    public void Food_ShouldAddFoodToDatabaseAndRemove()
    {
        var foodMacronutrient = new Macronutrient(10, 20, 1, 150);
        var food = new Food("Apple", foodMacronutrient, 60);
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
        var food = new Food("Chicken egg",  foodMacronutrient,60);
        var foodProduct = new FoodProductPiece(food, 5, DayOfWeek.Monday, _testTrack.Id, false);
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
        var food = new Food("Water", foodMacronutrient,100);
        var foodProduct = new FoodProductGram(food, 2000, DayOfWeek.Monday, _testTrack.Id, false);
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
        var chickenBreast = new Food("Куриная грудка", chickenBreastMacronutrients, 100);

        var riceMacronutrients = new Macronutrient(2, 29, 0, 130);
        var rice = new Food("Рис", riceMacronutrients, 100);

        var saladMacronutrients = new Macronutrient(1, 3, 0, 15);
        var salad = new Food("Салат", saladMacronutrients, 100);

        var salmonMacronutrients = new Macronutrient(13, 0, 20, 206);
        var salmon = new Food("Лосось", salmonMacronutrients, 100);

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.Foods.AddRange(new List<Food>() { chickenBreast, rice, salad, salmon });
            context.SaveChanges();
        }
        
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.FoodProductsGrams.Add(new FoodProductGram(chickenBreast, 200, DayOfWeek.Wednesday, track,false));
            context.FoodProductsGrams.Add(new FoodProductGram(rice, 150, DayOfWeek.Wednesday, track, false));
            context.FoodProductsPieces.Add(new FoodProductPiece(salad, 1, DayOfWeek.Thursday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(chickenBreast, 150, DayOfWeek.Thursday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(rice, 100, DayOfWeek.Thursday, track, false));
            context.FoodProductsGrams.Add(new FoodProductGram(salmon, 170, DayOfWeek.Sunday, track, false));
            context.SaveChanges();
        }
    }
}