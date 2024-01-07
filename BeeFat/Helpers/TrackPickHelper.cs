using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Repositories;
using Blazorise;

namespace BeeFat.Helpers;

public class TrackPickHelper
{
    public Modal MyModal = default!;
    public Track SelectedTrack;
    public UserRepository UserRepository;
    public TrackRepository TrackRepository;
    public JournalRepository JournalRepository;
    public HashSet<Guid> RecommendentTracks;

    public TrackPickHelper(UserRepository userRepository, TrackRepository trackRepository, JournalRepository journalRepository)
    {
        RecommendentTracks = new HashSet<Guid>();
        JournalRepository = journalRepository;
        TrackRepository = trackRepository;
        UserRepository = userRepository;
    }

    public void Save(ApplicationUser user)
    {
        user.TrackId = SelectedTrack.Id;
        user.Track = TrackRepository.GetById(SelectedTrack.Id);
        UserRepository.Update(user);
        JournalRepository.UpdateByChangingUserTrack(user);
    }

    public void ChangeSelectedTrack(Track track)
    {
        SelectedTrack = track;
    }
    
    public IEnumerable<Track> CollectSuitableTracks(ApplicationUser user, int take = 4)
    {
        var metabolism = MetabolismCalculator.CalculateMetabolism(user);
        return TrackRepository.GetCollection(_ => true).OrderBy(t => Math.Abs(t.CaloriesByDay - metabolism)).Take(take);
    }

    public IEnumerable<Track> SearchTrack(ApplicationUser user, string searchValue)
    {
        if (searchValue == "")
        {
            var result = CollectSuitableTracks(user).ToList();
            RecommendentTracks.Clear();
            foreach (var track in result)
            {
                RecommendentTracks.Add(track.Id);
            }
            return CollectSuitableTracks(user);
        }
        return TrackRepository.GetCollection(t => t.Name.ToLower().Contains(searchValue.ToLower()));
    }
}