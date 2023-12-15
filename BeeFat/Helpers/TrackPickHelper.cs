using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;
using Blazorise;

namespace BeeFat.Helpers;

public class TrackPickHelper
{
    public IBaseRepository Repo { get; }
    public ApplicationUser User;
    public Modal MyModal = default!;
    public Track SelectedTrack;

    public TrackPickHelper(IBaseRepository repo)
    {
        Repo = repo;
        User = Repo.User;
    }

    public void Save()
    {
        User.Track = SelectedTrack;
        Repo.UpdateUserInfo(User);
        MyModal.Close(CloseReason.UserClosing);
    }

    public void ChangeSelectedTrack(Track track)
    {
        SelectedTrack = track;
        
    }
}