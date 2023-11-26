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
            var userToDelete = context.BeeFatUsers.FirstOrDefault(u => u.UserName == "testuser");
            userToDelete.Should().NotBeNull();
            context.BeeFatUsers.Remove(userToDelete);
            context.SaveChanges();
            var deletedUser = context.BeeFatUsers.FirstOrDefault(u => u.UserName == "testuser");
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
                f.Carbohydrates == 20 &&
                f.Fats == 10 &&
                f.Proteins == 1 &&
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
    public void FoodProduct_ShouldAddFoodProductToDatabaseAndRemove()
    {
        var food = new Food("Water", 0, 0, 0, 1000);;
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.Foods.Add(food);
            context.SaveChanges();
        }

        var foodProduct = new FoodProduct(2, food.Id, DayOfWeek.Monday, false);
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            context.FoodProducts.Add(foodProduct);
            context.SaveChanges();
        }
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdFoodProduct = context.FoodProducts
                .Include(fp => fp.Food)
                .FirstOrDefault(fp => fp.Id == foodProduct.Id);
        
            createdFoodProduct.Should().NotBeNull();
            createdFoodProduct.Should().Match<FoodProduct>(p =>
                p.DayOfWeek == DayOfWeek.Monday &&
                p.Count == 2 &&
                p.Food.Name == "Water" &&
                p.Food.Carbohydrates == 0 &&
                p.Food.Fats == 0 &&
                p.Food.Proteins == 0 &&
                p.Food.Weight == 1000
            );
            
            context.FoodProducts.Remove(createdFoodProduct);
            context.SaveChanges();
        }
        
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var deletedFoodProduct = context.FoodProducts.FirstOrDefault(fp => fp.Id == foodProduct.Id);
            deletedFoodProduct.Should().BeNull();
        }
    }
    
    
    [Explicit]
    [Test]
    public void DeleteAllEntriesFromFoodProducts()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var foodProducts = context.FoodProducts.ToList();
            context.FoodProducts.RemoveRange(foodProducts);
            context.SaveChanges();
        }
    }
}