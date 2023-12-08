using System.ComponentModel.DataAnnotations.Schema;
using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BeeFat.Data
{
    public class ApplicationUser : IdentityUser
    {
        public required PersonName PersonName { get; set; }
        
        public Guid TrackId { get; set; }
        
        [ForeignKey("TrackId")]
        public Track Track { get; set; }
        
        public required int Weight { get; set; }
        
        public required int Height { get; set; }
        
        public required int Age { get; set; }
        
        public required int RightCalories { get; set; }

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