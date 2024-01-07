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

    public override IEnumerable<Journal> GetCollection(Func<Journal, bool> selector)
    {
        throw new NotImplementedException();
    }

    public void UpdateByChangingUserTrack(ApplicationUser user)
    {
        Track track;
        using (var context = _context)
        {
            var foundUser1 = context.BeeFatUsers
                .Include(u => u.Track)
                .ThenInclude(j => j.FoodProducts)
                .ThenInclude(fp => fp.Food)
                .First(u => u.Id == user.Id);
            track = foundUser1.Track;
        }
        
        Journal journal;
        using (var context = _context)
        {
            var foundUser = context.BeeFatUsers
                .Include(u => u.Journal)
                .ThenInclude(j => j.FoodProducts)
                // .ThenInclude(fp => fp.Macronutrient)
                // .Include(u => u.Track)
                // .ThenInclude(j => j.FoodProducts)
                // .ThenInclude(fp => fp.Food)
                .First(u => u.Id == user.Id);
            journal = foundUser.Journal;
        }
        
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

    public void UpdatePortionSize(Journal userJournal, FoodProduct selectedFoodProduct)
    {
        using var context = _context;
        
        var existingFoodProduct = userJournal.FoodProducts.First(fp => fp.Id == selectedFoodProduct.Id);
        existingFoodProduct.PortionSize = selectedFoodProduct.PortionSize;
        existingFoodProduct.IsEaten = selectedFoodProduct.IsEaten;
        context.SaveChanges();
    }
}