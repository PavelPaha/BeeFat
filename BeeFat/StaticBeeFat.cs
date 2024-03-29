using BeeFat.Domain.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace BeeFat;

public static class StaticBeeFat
{
    public static IEnumerable<FoodPair> MergeProductsFromTrackAndJournal(List<FoodProduct> fpFromTrack,
        List<JournalFood> fpFromJournal)
    {
        var fpt = fpFromTrack;
        var fpj = fpFromJournal;
        var result = new List<FoodPair>();
        var fpToRemove = new List<JournalFood>();
        foreach (var p in fpj)
        {
            var foundFpt = fpt.FirstOrDefault(fp => fp.Id == p.FoodProductReference);
            if (!(foundFpt is null))
            {
                result.Add(new FoodPair(p, foundFpt));
                fpToRemove.Add(p);
            }
        }

        foreach (var fp in fpToRemove)
        {
            fpj.Remove(fp);
        }
        return result;
    }
    
    public static void RedirectTo(this NavigationManager navigationManager, string url)
    {
        navigationManager.NavigateTo(url, forceLoad: true);
    }
    
    public static IEnumerable<DayOfWeek> GetDays(int first = 1, int last = 7)
    {
        var days = Enum.GetValues(typeof(DayOfWeek));
        for (var i = first; i <= last; i++)
        {
            yield return (DayOfWeek)days.GetValue(i % 7);
        }
    }

    public static Dictionary<string, double> ActivityToLevel = new()
    {
        {"Малоподвижный", 1.2},
        {"Низкий", 1.375},
        {"Умеренный", 1.55},
        {"Высокий", 1.725},
        {"Очень высокий", 1.9}
    };
    
    public static Dictionary<DayOfWeek, string> NumberToDay = new()
    {
        { DayOfWeek.Monday, "Понедельник" },
        { DayOfWeek.Tuesday, "Вторник" },
        { DayOfWeek.Wednesday, "Среда" },
        { DayOfWeek.Thursday, "Четверг" },
        { DayOfWeek.Friday, "Пятница" },
        { DayOfWeek.Saturday, "Суббота" },
        { DayOfWeek.Sunday, "Воскресенье" }
    };
    
    public static Dictionary<double, string> LevelToActivity 
        = ActivityToLevel.ToDictionary(x => x.Value, x => x.Key);

    public static DayOfWeek Today = DayOfWeek.Monday;
}