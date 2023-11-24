using BeeFat.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BeeFat.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(PersonName personName)
        {
            PersonName = personName;
        }
        
        public ApplicationUser()
        {
        }

        public PersonName PersonName { get; set; }
    }

}
