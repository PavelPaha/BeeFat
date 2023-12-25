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
            .ThenInclude(fp => fp.Food)
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
        Journal journal;
        Track track;
        using var context = _context;

        var foundUser = context.BeeFatUsers
            .Include(u => u.Journal)
            .ThenInclude(j => j.FoodProducts)
            .ThenInclude(fp => fp.Food)
            .First(u => u.Id == user.Id);
        journal = foundUser.Journal;

        var foundUser1 = context.BeeFatUsers
            .Include(u => u.Track)
            .ThenInclude(j => j.FoodProducts)
            .ThenInclude(fp => fp.Food)
            .First(u => u.Id == user.Id);
        track = foundUser.Track;

        foreach (var fp in journal.FoodProducts)
        {
            context.FoodProducts.Remove(fp);
            context.SaveChanges();
        }

        RemoveAllFoodProductsFromJournal(journal);
        context.SaveChanges();
        AddFoodProductsFromTrackToJournal(track, journal);
        context.SaveChanges();
    }

    private void RemoveAllFoodProductsFromJournal(Journal journal)
    {
        journal.FoodProducts.Clear();
    }

    private void AddFoodProductsFromTrackToJournal(Track track, Journal journal)
    {
        foreach (var fp in track.FoodProducts)
        {
            FoodProduct fpToAdd;

            switch (fp)
            {
                case FoodProductGram fpg:
                    fpToAdd = new FoodProductGram(fpg.Food, fpg.Grams, fpg.DayOfWeek, fpg.Track, fpg.IsEaten);
                    break;
                case FoodProductPiece fpp:
                    fpToAdd = new FoodProductPiece(fpp.Food, fpp.Pieces, fpp.DayOfWeek, fpp.Track, fpp.IsEaten);
                    break;
                default:
                    throw new Exception($"Неизвестный тип продукта {fp.Name}");
            }

            journal.FoodProducts.Add(fpToAdd);
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