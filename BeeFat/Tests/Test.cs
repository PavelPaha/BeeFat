using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeeFat.Data;

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
        using (var context = new ApplicationDbContext(_options, _configuration))
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
            context.BeeFatUsers.Add(user);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdUser = context.BeeFatUsers.FirstOrDefault();
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
    
    [Test]
    public void Food_ShouldAddFoodToDatabaseAndRemove()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var food = new Food("Apple", 10, 20, 1, 150);
            context.Foods.Add(food);
            context.SaveChanges();
        }
        
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var createdFood = context.Foods.FirstOrDefault(f => f.Name == "Apple");
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
            var foodToDelete = context.Foods.FirstOrDefault(u => u.Name == "Apple");
            foodToDelete.Should().NotBeNull();
            context.Foods.Remove(foodToDelete);
            context.SaveChanges();
            var deletedFood = context.Foods.FirstOrDefault(u => u.Name == "Apple");
            deletedFood.Should().BeNull();
        }
    }

    [Test]
    public void FoodProduct_ShouldAddFoodProductToDatabaseAndRemove()
    {
        FoodProduct foodProduct = null;
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var food = new Food("Water", 0, 0, 0, 1000);
            foodProduct = new FoodProduct { Count = 2, Food = food, IsEaten = false };
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

    
    [Test]
    public void T()
    {
        using (var context = new ApplicationDbContext(_options, _configuration))
        {
            var food = context.Foods.FirstOrDefault();

            if (food != null)
            {
                var foodProduct = new FoodProduct
                {
                    Count = 2,
                };
                
                context.FoodProducts.Add(foodProduct);
                context.SaveChanges();
                foodProduct.Food = food;
                context.SaveChanges();
            }
        }


    }
}