using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Domain.Infrastructure;

public abstract class JournalFood: Entity
{
    public required string Name { get; set; }

    public Guid? JournalId { get; set; }
    [ForeignKey("JournalId")] public Journal Journal { get; set; }

    public Guid FoodProductReference { get; set; }

    public required Macronutrient Macronutrient { get; set; }
    public DayOfWeek DayOfWeek { get; set; }

    public bool IsEaten { get; set; }

    public int PortionSize { get; set; }

    public virtual double PortionCoeff => (double)PortionSize / 100;

    [SetsRequiredMembers]
    protected JournalFood()
    {
    }


    [SetsRequiredMembers]
    protected JournalFood(string name, Journal journal, DayOfWeek dayOfWeek, Macronutrient macronutrient, int portionSize, bool isEaten)
    {
        Macronutrient = macronutrient;
        Name = name;
        JournalId = journal.Id;
        DayOfWeek = dayOfWeek;
        PortionSize = portionSize;
        IsEaten = isEaten;
    }
}