using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Repositories;

public class JournalRepository : Repository<Journal>
{
    public JournalRepository(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(
        configuration, options)
    {
    }

    public override Journal GetById(Guid id)
    {
        using var context = _context;
        return context.Journals
            .Include(j => j.FoodProducts)
            .ThenInclude(fp => fp.Macronutrient)
            .First(j => j.Id == id);
    }

    public override bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override void Update(Journal entity)
    {
        throw new NotImplementedException();
    }

    public void Add(Journal entity)
    {
        var context = _context;
        context.Journals.Add(entity);
        context.SaveChanges();
    }

    public override IEnumerable<Journal> GetCollection(Func<Journal, bool> selector)
    {
        throw new NotImplementedException();
    }

    public void UpdateByChangingUserTrack(Guid trackId, Guid journalId)
    {
        Track track;
        using (var context = _context)
        {
            track = context.Tracks.Include(track => track.FoodProducts).ThenInclude(foodProduct => foodProduct.Food)
                .ThenInclude(food => food.Macronutrient).First(t => t.Id == trackId);
        }

        Journal journal;
        using (var context = _context)
        {
            journal = context.Journals.Include(journal => journal.FoodProducts).First(j => j.Id == journalId);
        }
        UpdateByChangingUserTrack(track, journal);
    }

    public void UpdateByChangingUserTrack(Track track, Journal journal)
    {
        using (var context = _context)
        {
            foreach (var jfp in journal.FoodProducts)
            {
                context.JournalFoods.Remove(jfp);
                context.SaveChanges();
            }
            RemoveAllFoodProductsFromJournal(journal);
            context.SaveChanges();
            AddFoodProductsFromTrackToJournal(context, track, journal);
            context.SaveChanges();
        }
    }

    private void RemoveAllFoodProductsFromJournal(Journal journal)
    {
        journal.FoodProducts.Clear();
    }

    private void AddFoodProductsFromTrackToJournal(ApplicationDbContext context, Track track, Journal journal)
    {
        foreach (var fp in track.FoodProducts)
        {
            JournalFood fpToAdd;
            var macronutrient = new Macronutrient(fp.Food.Macronutrient);
            switch (fp)
            {
                case FoodProductGram fpg:
                    fpToAdd = new JournalFoodGram(fpg.Name, macronutrient, fpg.DayOfWeek, journal, 0, fpg.IsEaten);
                    break;
                case FoodProductPiece fpp:
                    fpToAdd = new JournalFoodPiece(fpp.Name, macronutrient, fpp.Pieces, fpp.DayOfWeek, journal, 0, fpp.IsEaten);
                    break;
                default:
                    throw new Exception($"Неизвестный тип продукта {fp.Name}");
            }

            fpToAdd.FoodProductReference = fp.Id;
            context.JournalFoods.Add(fpToAdd);
        }
    }
}