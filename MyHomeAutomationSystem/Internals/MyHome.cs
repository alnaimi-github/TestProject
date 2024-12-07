using System.Reactive.Concurrency;

namespace MyHomeAutomationSystem.Internals;

public class MyHome
{
    public readonly  IObservable<IEntity> StateChanges;
    private readonly IScheduler           _scheduler;
    public           List<Announcement>   Announcements = new();
    public           List<IEntity>        Entities      = new();
    public           List<Prompt>         Prompts       = new();

    public MyHome(IObservable<IEntity> stateChanges, IScheduler scheduler)
    {
        StateChanges = stateChanges;
        _scheduler   = scheduler;
        StateChanges.Subscribe(entity =>
        {
            //Console.WriteLine($"--> State Change: '{entity.Name}' : '{entity.State}'");
            Entities.RemoveAll(e => e.Name == entity.Name);
            Entities.Add(entity);
        });
    }

    public DateTime CurrentTime => _scheduler.Now.DateTime;

    public void Announce(Announcement announcement)
    {
        //Console.WriteLine($"--> Announcement: '{announcement.SpeakerName}' : '{announcement.Message}'");
        Announcements.Add(announcement);
    }

    public void Prompt(Prompt prompt)
    {
        //Console.WriteLine($"--> Prompt: '{prompt.SpeakerName}' : '{prompt.Message}'");
        Prompts.Add(prompt);
    }

    public Prompt LastPrompt(string speakerName) => Prompts.Any() ? Prompts.Last(p => p.SpeakerName == speakerName) : new Prompt(speakerName, "", _scheduler.Now.Date.AddMinutes(-60));
}

