using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Interfaces;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

public class TrackPickHelper
{
    public ApplicationUser User;
    public Modal MyModal = default!;
    public Track SelectedTrack;
    public UserRepository UserRepository;
    public TrackRepository TrackRepository;

    public Guid _id = FakeData.HardId;

    public TrackPickHelper(UserRepository userRepository, TrackRepository trackRepository)
    {
        TrackRepository = trackRepository;
        UserRepository = userRepository;
        User = userRepository.FetchUserInfo(_id);
    }

    public void Save()
    {
        User.TrackId = SelectedTrack.Id;
        User.Track = SelectedTrack;
        UserRepository.Update(User);
    }

    public void ChangeSelectedTrack(Track track)
    {
        SelectedTrack = track;
    }
}