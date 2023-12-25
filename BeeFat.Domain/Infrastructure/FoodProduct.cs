using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public abstract class FoodProduct : Entity
{
    public required string Name { get; set; }

    public Guid TrackId { get; set; }
    [ForeignKey("TrackId")] 
    public required Track Track { get; set; }
    
    public Guid? JournalId { get; set; }
    [ForeignKey("JournalId")] 
    public Journal Journal { get; set; }

    public Guid FoodId { get; set; }
    [ForeignKey("FoodId")] 
    public required Food Food { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public bool IsEaten { get; set; }

    public abstract int PortionSize { get; set; }

    public double PortionCoeff => (double)PortionSize / 100;

    [SetsRequiredMembers]
    protected FoodProduct() { }

    [SetsRequiredMembers]
    protected FoodProduct(Food food, DayOfWeek dayOfWeek, Track track, bool isEaten)
        : this(food, dayOfWeek, track.Id, isEaten)
    {
        Track = track;
    }

    [SetsRequiredMembers]
    protected FoodProduct(Food food, DayOfWeek dayOfWeek, Guid trackId, bool isEaten)
    {
        Food = food;
        FoodId = food.Id;
        DayOfWeek = dayOfWeek;
        TrackId = trackId;
        IsEaten = isEaten;
    }
    public abstract void ChangePortionSize(int newPortionSize);

    protected virtual void EnsurePortionSize(int portionSize)
    {
        if (portionSize < 0)
            throw new ArgumentException("Размер порции должен быть неотрицательным");
    }
}