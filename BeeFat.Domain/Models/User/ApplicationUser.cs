using System.ComponentModel.DataAnnotations.Schema;
using BeeFat.Domain.Infrastructure;
using BeeFat.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace BeeFat.Data
{
    public class ApplicationUser : IdentityUser
    {
        public required PersonName PersonName { get; set; }

        public Guid TrackId { get; set; }

        [ForeignKey("TrackId")] public Track Track { get; set; }


        public Guid JournalId { get; set; }

        [ForeignKey("JournalId")] public required Journal Journal { get; set; }

        public required int Weight { get; set; }

        public required int Height { get; set; }

        public required int Age { get; set; }


        public required Gender Gender { get; set; }

        public required double ActivityLevel { get; set; }

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
            Gender = otherUser.Gender;
            ActivityLevel = otherUser.ActivityLevel;
            // Track = otherUser.Track;
            Weight = otherUser.Weight;
            Height = otherUser.Height;
            Age = otherUser.Age;
            // Journal = otherUser.Journal;
        }

        [SetsRequiredMembers]
        public ApplicationUser()
        {
        }
    }
}

public enum Gender
{
    Male,
    Female
}