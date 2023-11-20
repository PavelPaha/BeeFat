using System.Diagnostics.CodeAnalysis;
using BeeFat.Domain.Infrastructure;

namespace BeeFat.Domain.Models.User;

public class User : Entity<uint>
{
    public required string Email { get; set; }

    [SetsRequiredMembers]
    public User(uint id, string email) : base(id)
    {
        Email = email;
    }
}