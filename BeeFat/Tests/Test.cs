// using BeeFat.Domain.Models.User;
// using FluentAssertions;
// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
//
// namespace BeeFat.Data;
//
// [TestFixture]
// public class ApplicationDbContextTests
// {
//     private IConfiguration _configuration;
//
//     private DbContextOptions<ApplicationDbContext> GetOptions()
//     {
//         return new DbContextOptionsBuilder<ApplicationDbContext>()
//             .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
//             .Options;
//     }
//
//     [SetUp]
//     public void Setup()
//     {
//         var configuration = new ConfigurationBuilder()
//             .AddJsonFile("appsettings.json")
//             .Build();
//
//         _configuration = configuration;
//     }
//
//     [Test]
//     public void CreateUser_ShouldAddUserToDatabase()
//     {
//         var options = GetOptions();
//         using (var context = new ApplicationDbContext(options, _configuration))
//         {
//             var user = new ApplicationUser()
//             {
//                 UserName = "testuser",
//                 PersonName = new PersonName
//                 {
//                     FirstName = "Кирилл",
//                     LastName = "Сарычев"
//                 }
//             };
//             context.BeeFatUsers.Add(user);
//             context.SaveChanges();
//         }
//
//         using (var context = new ApplicationDbContext(options, _configuration))
//         {
//             var createdUser = context.BeeFatUsers.FirstOrDefault();
//             createdUser.Should().NotBeNull();
//             createdUser.UserName.Should().Be("testuser");
//             createdUser.PersonName.FirstName.Should().Be("Кирилл");
//             createdUser.PersonName.LastName.Should().Be("Сарычев");
//         }
//     }
// }