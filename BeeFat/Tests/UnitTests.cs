using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Helpers;
using BeeFat.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BeeFat.Tests;

[TestFixture]
public class UnitTests
{
    private IConfiguration configuration;
    private DbContextOptions<ApplicationDbContext> options;

    public UserRepository UserRepository;
    public TrackRepository TrackRepository;
    public JournalRepository JournalRepository;
    public FoodProductRepository FoodProductRepository;
    public JournalFoodRepository JournalFoodRepository;

    private DbContextOptions<ApplicationDbContext> GetOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;
    }

    public UnitTests()
    {
        configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        options = GetOptions();
        UserRepository = new UserRepository(configuration, options);
        TrackRepository = new TrackRepository(configuration, options);
        JournalRepository = new JournalRepository(configuration, options);
        FoodProductRepository = new FoodProductRepository(configuration, options);
        JournalFoodRepository = new JournalFoodRepository(configuration, options);
    }

    [Test]
    public void TestUserInfoSaving()
    {
        var hh = new HomeHelper(UserRepository, JournalRepository, JournalFoodRepository);
        var trackPicker = new TrackPickHelper(UserRepository, TrackRepository, JournalRepository);

        var newTrack = TrackRepository.GetCollection(t => true).First();
        trackPicker.ChangeSelectedTrack(newTrack);
        trackPicker.Save();

        var userProfileHelper = new UserProfileHelper(UserRepository, TrackRepository);
        var newLastName = GenerateRandomString(10);
        userProfileHelper.UserModel.PersonName.LastName = newLastName;
        userProfileHelper.SaveProfile();

        var hh1 = new HomeHelper(UserRepository, JournalRepository, JournalFoodRepository);
        hh1.User.TrackId.Should().Be(newTrack.Id);
        hh1.User.PersonName.LastName.Should().Be(newLastName);
    }

    [Test]
    public void TestTransferFoodProductsFromTrackToJournal()
    {
        var hh = new HomeHelper(UserRepository, JournalRepository, JournalFoodRepository);
        var track = hh.User.Track;
        
        Track otherTrack;
        using (var context = new ApplicationDbContext(options, configuration))
        {
            otherTrack = context.Tracks.First();
        }

        var tp = new TrackPickHelper(UserRepository, TrackRepository, JournalRepository);
        tp.ChangeSelectedTrack(otherTrack);
        tp.Save();

        hh.FetchUserInfo();

        var journal = hh.User.Journal;
        var names1 = journal.FoodProducts.Select(fp => fp.Name).OrderBy(name => name).ToList();
        var names2 = track.FoodProducts.Select(fp => fp.Name).OrderBy(name => name).ToList();

        names1.SequenceEqual(names2).Should().BeTrue();

        for (var i = 0; i <= 6; i++)
        {
            var productsByDay = hh.GetProductsByDay(journal.FoodProducts, (DayOfWeek)i);
            var teh = new TrackViewerHelper(new TrackPickHelper(UserRepository, TrackRepository, JournalRepository), TrackRepository);
            var foodProducts = teh.GetProductsByDay(track.FoodProducts, (DayOfWeek)i);

            var macronutrientsByDay1 = productsByDay.Select(jp => jp.Macronutrient).OrderBy(m => m).ToList();
            var macronutrientsByDay2 = foodProducts.Select(jp => jp.Food.Macronutrient).OrderBy(m => m).ToList();

            names1.Count.Should().Be(names2.Count);
            foreach (var (n1, n2) in macronutrientsByDay1.Zip(macronutrientsByDay2, (m1, m2) => (m1, m2)))
            {
                n1.Should().BeEquivalentTo(n2);
            }
        }
    }

    [Test]
    public void TestSetEatenFoodProducts()
    {
        var hh = new HomeHelper(UserRepository, JournalRepository, JournalFoodRepository);
        var journal = hh.User.Journal;
        var eatenProduct = journal.FoodProducts.First(fp => !fp.IsEaten);
        hh.SelectedJournalFood = eatenProduct;
        eatenProduct.PortionSize += 10;
        hh.PortionSize = eatenProduct.PortionSize;

        hh.ChangeFoodProductInfoAndSave(true);
        journal = hh.JournalRepository.GetById(journal.Id);
        var foundEatenProduct = journal.FoodProducts.First(fp => fp.Id == eatenProduct.Id);
        foundEatenProduct.IsEaten.Should().BeTrue();
        foundEatenProduct.PortionSize.Should().Be(hh.PortionSize);
    }
    
    


    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();

        var randomString = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return randomString;
    }
}