namespace MyHomeAutomationSystem.Internals;

public class Announcement
{
    public string Message { get; }
    public DateTime Sent { get; }
    public string SpeakerName { get; }


    public Announcement(string speakerName, string message, DateTime sent)
    {
        SpeakerName = speakerName;
        Message     = message;
        Sent        = sent;
    }
}