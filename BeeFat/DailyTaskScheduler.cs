using BeeFat.Repositories;

namespace BeeFat;

public class DailyTaskScheduler
{
    private Timer _timer;
    private JournalRepository _journalRepository;
    private UserRepository _userRepository;

    public DailyTaskScheduler(JournalRepository journalRepository, UserRepository userRepository)
    {
        _journalRepository = journalRepository;
        _userRepository = userRepository;
    }

    public async Task Start()
    {
        var now = DateTime.Now;
        var nextMidnight = now.AddDays(1).Date;
        var timeUntilMidnight = nextMidnight.Subtract(now);
        await Task.Delay(timeUntilMidnight);

        IncrementDay();
        
        await Start();
    }

    private void RemoveJournalStory()
    {
        var users = _userRepository.GetCollection(_ => true);
        foreach (var user in users)
        {
            _userRepository.RemoveUserJournalStory(user.Id);
        }
    }

    public void IncrementDay()
    {
        if (StaticBeeFat.Today != DayOfWeek.Saturday)
            StaticBeeFat.Today += 1;
        else StaticBeeFat.Today = DayOfWeek.Sunday;
        
        if (StaticBeeFat.Today == DayOfWeek.Monday)
        {
            RemoveJournalStory();
        }
    }
}