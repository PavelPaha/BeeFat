using System.Diagnostics.CodeAnalysis;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Domain.Models;

public class Track : Entity<uint>
{
    public required string Title { get; set; }
    public required string Description { get; set; }

    public required List<Day> Days { get; set; }

    [SetsRequiredMembers]
    public Track(uint id, string title, string description) : base(id)
    {
        Title = title;
        Description = description;
        Days = Enumerable.Range(0, 7)
            .Select(i => new Day((DayOfWeek)i))
            .ToList();
    }
}

//TODO удалить, когда появится класс Day
public record Day(DayOfWeek DayOfWeek);