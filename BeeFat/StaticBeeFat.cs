using BeeFat.Domain.Infrastructure;

namespace BeeFat;

public static class StaticBeeFat
{
    public static IEnumerable<FoodPair> MergeProductsFromTrackAndJournal(IEnumerable<FoodProduct> fpFromTrack,
        IEnumerable<JournalFood> fpFromJournal)
    {
        var fpt = fpFromTrack.ToList();
        var fpj = fpFromJournal.ToList();
        var result = new List<FoodPair>();
        foreach (var p in fpj)
        {
            var foundFpt = fpt.FirstOrDefault(fp => fp.Id == p.FoodProductReference);
            if (!(foundFpt is null))
            {
                result.Add(new FoodPair(p, foundFpt));
                fpt.Remove(foundFpt);
            }
        }
        return result;
    }
}