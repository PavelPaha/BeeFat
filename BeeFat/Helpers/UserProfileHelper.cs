using BeeFat.Data;
using BeeFat.Interfaces;
using Blazorise;

namespace BeeFat.Helpers;

public class UserProfileHelper
{
    public IBaseRepository Repo { get; }
    public Modal Modal { get; set; }
    public ApplicationUser User;
    public ApplicationUser UserModel;

    public UserProfileHelper(IBaseRepository repo)
    {
        Repo = repo;
        Modal = default!;
        User = Repo.GetUser();
        UserModel = new ApplicationUser(User);
    }

    public void SaveProfile()
    {
        User.CloneFrom(UserModel);
        Repo.UpdateUserInfo(User);
    }
}