using BeeFat.Domain.Infrastructure;

namespace BeeFat.Domain.Models.User;

public class User : Entity
{
    public string Email { get; set; }
    public PersonName? PersonName { get; set; }

    public Track? Track { get; set; }

    public User(string id, string email) : base(id)
    {
        Email = email;
    }
}