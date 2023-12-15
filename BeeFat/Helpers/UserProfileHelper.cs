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
        User = Repo.User;
        UserModel = User;
    }

    public void SaveProfile()
    {
        Repo.UpdateUserInfo(User);
    }
}