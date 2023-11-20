using System.Diagnostics.CodeAnalysis;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Domain.Models.User;

[method: SetsRequiredMembers]
public class PersonName(string firstName, string lastName) : ValueType<PersonName>
{
    public required string FirstName { get; set; } = firstName;
    public required string LastName { get; set; } = lastName;
}