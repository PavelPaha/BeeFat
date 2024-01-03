using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
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
    public HashSet<Guid> RecommendentTracks;

    public Guid _id = FakeData.HardId;
    

    public TrackPickHelper(UserRepository userRepository, TrackRepository trackRepository, JournalRepository journalRepository)
    {
        RecommendentTracks = new HashSet<Guid>();
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

    public IEnumerable<Track> SearchTrack(string searchValue)
    {
        if (searchValue == "")
        {
            var result = CollectSuitableTracks().ToList();
            RecommendentTracks.Clear();
            foreach (var track in result)
            {
                RecommendentTracks.Add(track.Id);
            }
            return CollectSuitableTracks();
        }
        return TrackRepository.GetCollection(t => t.Name.ToLower().Contains(searchValue.ToLower()));
    }
}