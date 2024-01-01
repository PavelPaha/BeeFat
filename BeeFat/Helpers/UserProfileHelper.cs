using BeeFat.Data;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

public class UserProfileHelper
{
    public Modal Modal { get; set; }
    public ApplicationUser User;
    public ApplicationUser UserModel;

    public UserRepository UserRepository;
    public Guid _id = FakeData.HardId;

    public UserProfileHelper(UserRepository userRepository)
    {
        UserRepository = userRepository;
        Modal = default!;
        User = userRepository.GetById(_id);
        UserModel = User;
    }

    public void SaveProfile()
    {
        UserRepository.Update(UserModel);
    }
}