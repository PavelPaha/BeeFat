using System.Diagnostics.CodeAnalysis;
using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Domain.Models.User;

[method: SetsRequiredMembers]
public class PersonName : ValueType<PersonName>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}