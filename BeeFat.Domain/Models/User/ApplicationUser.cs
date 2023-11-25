using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BeeFat.Data
{
    public class ApplicationUser : IdentityUser
    {
        public PersonName PersonName { get; set; }

        public ApplicationUser(PersonName personName)
        {
            PersonName = personName;
        }

        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}