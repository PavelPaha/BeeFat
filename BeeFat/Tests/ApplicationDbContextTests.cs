using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeeFat.Tests;

[TestFixture]
[Parallelizable]
public class ApplicationDbContextTests
{
    private IConfiguration _configuration;
    private DbContextOptions<ApplicationDbContext> _options;

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
    }


    [Test]
    public void User_ShouldAddUserToDatabaseAndRemove()
    {
        var user = new ApplicationUser()
        {
            UserName = "testuser",
            PersonName = new PersonName
            {
                FirstName = "Кирилл",
                LastName = "Сарычев"
            }
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
                user.UserName == "testuser" &&
                user.PersonName.FirstName == "Кирилл" &&
                user.PersonName.LastName == "Сарычев"
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


    [Explicit]
    [Test]
    public void DeleteAllEntriesFromUsers()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var users = context.Users.ToList();
            context.Users.RemoveRange(users);
            context.SaveChanges();
        }
    }


    [Test]
    public void Food_ShouldAddFoodToDatabaseAndRemove()
    {
        var food = new Food("Apple", 10, 20, 1, 150);
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
                f.Proteins == 10 &&
                f.Fats == 20 &&
                f.Carbohydrates == 1 &&
                f.Weight == 150
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
            var foods = context.Foods.ToList();
            context.Foods.RemoveRange(foods);
            context.SaveChanges();
        }
    }


    [Test]
    public void FoodProductPiece_ShouldAddFoodProductPieceToDatabaseAndRemove()
    {
        var food = new Food("Chicken egg", 7, 1, 7, 60);
        var foodProduct = new FoodProductPiece(food, 5, DayOfWeek.Monday, false);
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
                p.Food.Proteins == 7 &&
                p.Food.Fats == 1 &&
                p.Food.Carbohydrates == 7 &&
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
        var food = new Food("Water", 0, 0, 0, 1);
        var foodProduct = new FoodProductGram(food, 2000, DayOfWeek.Monday, false);
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
                p.Food.Carbohydrates == 0 &&
                p.Food.Fats == 0 &&
                p.Food.Proteins == 0 &&
                p.Food.Weight == 1
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
}