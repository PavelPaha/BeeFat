using System.ComponentModel.DataAnnotations.Schema;
using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Data
{
    public class ApplicationUser : Entity
    {
        public required PersonName PersonName { get; set; }
        
        public Guid TrackId { get; set; }
        
        [ForeignKey("TrackId")]
        public Track Track { get; set; }
        
        public required int Weight { get; set; }
        
        public required int Height { get; set; }
        
        public required int Age { get; set; }
        
        public required int RightCalories { get; set; }
        
        public required Journal Journal { get; set; }

        [SetsRequiredMembers]
        public ApplicationUser(PersonName personName, Track track)
            : this(personName, track.Id)
        {
            Track = track;
        }

        [SetsRequiredMembers]
        public ApplicationUser(PersonName personName, Guid trackId)
        {
            PersonName = personName;
            TrackId = trackId;
        }

        [SetsRequiredMembers]
        public ApplicationUser(ApplicationUser otherUser)
        {
            CloneFrom(otherUser);
        }

        public void CloneFrom(ApplicationUser otherUser)
        {
            if (PersonName is null)
            {
                PersonName = new PersonName(otherUser.PersonName);
            }
            else
            {
                PersonName.FirstName = otherUser.PersonName.FirstName;
                PersonName.LastName = otherUser.PersonName.LastName;
            }
            TrackId = otherUser.TrackId;
            Track = otherUser.Track;
            Weight = otherUser.Weight;
            Height = otherUser.Height;
            Age = otherUser.Age;
            RightCalories = otherUser.RightCalories;
            Journal = otherUser.Journal;
        }

        [SetsRequiredMembers]
        public ApplicationUser()
        {
        }
    }
}