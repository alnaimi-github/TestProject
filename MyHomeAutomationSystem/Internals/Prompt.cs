namespace MyHomeAutomationSystem.Internals;

public class Prompt
{
    public string   Message { get; }
    public DateTime Sent { get; }
    public string   SpeakerName { get; }
    public          DateTime Acknowledged;

    public Prompt(string speakerName, string message, DateTime sent)
    {
        SpeakerName = speakerName;
        Message     = message;
        Sent        = sent;
    }


    public void Acknowledge(DateTime acknowledged)
    {
        Acknowledged = acknowledged;
    }

    
}