using System.Diagnostics.CodeAnalysis;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Domain.Models.User;

public class PersonName : ValueType<PersonName>
{
    [SetsRequiredMembers]
    public PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [SetsRequiredMembers]
    public PersonName(PersonName otherPersonName)
    {
        FirstName = otherPersonName.FirstName;
        LastName = otherPersonName.LastName;
    }
    
    public PersonName() { }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}