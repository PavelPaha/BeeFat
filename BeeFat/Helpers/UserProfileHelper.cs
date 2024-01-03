using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

public class UserProfileHelper
{
    public Modal Modal { get; set; }
    public ApplicationUser User;
    public ApplicationUser UserModel;

    public UserRepository UserRepository;
    public TrackRepository TrackRepository;
    public Guid _id = FakeData.HardId;

    public string GenderString;
    public string ActivityString;

    public UserProfileHelper(UserRepository userRepository, TrackRepository trackRepository)
    {
        UserRepository = userRepository;
        TrackRepository = trackRepository;
        Modal = default!;
        User = userRepository.GetById(_id);
        ActivityString = StaticBeeFat.LevelToActivity[User.ActivityLevel];
        GenderString = User.Gender == Gender.Female ? "Female" : "Male";
        UserModel = User;
    }

    public void SaveProfile()
    {
        UserRepository.Update(UserModel);
    }

    public void Save()
    {
        var activityLevel = StaticBeeFat.ActivityToLevel[ActivityString];
        UserModel.ActivityLevel = activityLevel;
        if (GenderString == "Female")
            UserModel.Gender = Gender.Female;
        else if (GenderString == "Male")
            UserModel.Gender = Gender.Male;
        SaveProfile(); 
        Modal.Close(CloseReason.UserClosing);
    }

    public IEnumerable<Track> CollectSuitableTracks()
    {
        User = UserRepository.GetById(User.Id);
        var metabolism = CalculateMetabolism();
        return TrackRepository.GetCollection(_ => true).OrderBy(t => Math.Abs(t.CaloriesByDay - metabolism)).Take(4);
    }

    private int CalculateMetabolism()
    {
        if (User.Gender == Gender.Female)
            return
                MetabolismCalculator.CalculateBMRForFemaleWithActivity(User.Weight, User.Height, User.Age,
                    User.ActivityLevel);
        return MetabolismCalculator.CalculateBMRForMaleWithActivity(User.Weight, User.Height, User.Age,
            User.ActivityLevel);
    }
}