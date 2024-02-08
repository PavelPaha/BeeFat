using BeeFat.Data;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

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

    public void Save(ApplicationUser user, string genderString, string activityString)
    {
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
}