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
    public JournalRepository JournalRepository;

    public Guid _id = FakeData.HardId;

    public TrackPickHelper(UserRepository userRepository, TrackRepository trackRepository, JournalRepository journalRepository)
    {
        JournalRepository = journalRepository;
        TrackRepository = trackRepository;
        UserRepository = userRepository;
        User = userRepository.GetById(_id);
    }

    public void Save()
    {
        User.TrackId = SelectedTrack.Id;
        User.Track = TrackRepository.GetById(SelectedTrack.Id);
        UserRepository.Update(User);
        JournalRepository.UpdateByChangingUserTrack(User);
    }

    public void ChangeSelectedTrack(Track track)
    {
        SelectedTrack = track;
    }
}