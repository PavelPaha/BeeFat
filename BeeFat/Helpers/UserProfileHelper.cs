using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Components.Account.Domain.Helpers;

public class UserProfileHelper
{
    public Modal Modal { get; set; }
    public ApplicationUser UserModel { get; set; }

    public UserRepository UserRepository;
    public TrackRepository TrackRepository;

    public UserProfileHelper(UserRepository userRepository, TrackRepository trackRepository)
    {
        UserRepository = userRepository;
        TrackRepository = trackRepository;
        Modal = default!;
    }

    public string GetActivityString(ApplicationUser user) => StaticBeeFat.LevelToActivity[user.ActivityLevel];
    public string GetGenderString(ApplicationUser user) => user.Gender == Gender.Female ? "Female" : "Male";

    public void SaveProfile(ApplicationUser user)
    {
        UserRepository.Update(user);
    }

    public void Save(ApplicationUser user)
    {
        var activityString = GetActivityString(user);
        var genderString = GetGenderString(user);
        var activityLevel = StaticBeeFat.ActivityToLevel[activityString];
        user.ActivityLevel = activityLevel;
        user.Gender = genderString switch
        {
            "Female" => Gender.Female,
            "Male" => Gender.Male,
            _ => throw new NotSupportedException()
        };
        SaveProfile(user);
        Modal.Close(CloseReason.UserClosing);
    }

    public IEnumerable<Track> CollectSuitableTracks(ApplicationUser user)
    {
        var metabolism = CalculateMetabolism(user);
        return TrackRepository.GetCollection(_ => true).OrderBy(t => Math.Abs(t.CaloriesByDay - metabolism)).Take(4);
    }

    private int CalculateMetabolism(ApplicationUser user)
    {
        if (user.Gender == Gender.Female)
            return
                MetabolismCalculator.CalculateBMRForFemaleWithActivity(user.Weight, user.Height, user.Age,
                    user.ActivityLevel);
        return MetabolismCalculator.CalculateBMRForMaleWithActivity(user.Weight, user.Height, user.Age,
            user.ActivityLevel);
    }
}