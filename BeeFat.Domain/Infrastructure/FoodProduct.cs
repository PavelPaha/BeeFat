using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public abstract class FoodProduct : Entity
{
    public required string Name { get; set; }

    public Guid TrackId { get; set; }
    [ForeignKey("TrackId")] 
    public required Track Track { get; set; }

    public Guid FoodId { get; set; }
    [ForeignKey("FoodId")] 
    public required Food Food { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public bool IsEaten { get; set; }

    public abstract int PortionSize { get; set; }

    public virtual double PortionCoeff => (double)PortionSize / 100;

    [SetsRequiredMembers]
    protected FoodProduct() { }
    

    [SetsRequiredMembers]
    protected FoodProduct(Food food, DayOfWeek dayOfWeek, Track track, bool isEaten)
    {
        // Track = track;
        Food = food;
        FoodId = food.Id;
        DayOfWeek = dayOfWeek;
        TrackId = track.Id;
        IsEaten = isEaten;
    }
    
    [SetsRequiredMembers]
    protected FoodProduct(Food food, DayOfWeek dayOfWeek, Journal journal, bool isEaten)
    {
        // Journal = journal;
        Food = food;
        FoodId = food.Id;
        DayOfWeek = dayOfWeek;
        TrackId = journal.Id;
        IsEaten = isEaten;
    }
}