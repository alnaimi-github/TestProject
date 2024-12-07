using System.Reactive.Linq;
using MyHomeAutomationSystem.Internals;

namespace MyHomeAutomationSystem.Automations;

public sealed class BedroomAutomations
{
    public BedroomAutomations(MyHome home)
    {

        home.StateChanges.Where(s => s.Name == "motion.bedroom").Subscribe(_ =>
        {
            switch (home.CurrentTime.Hour)
            {
                case >= 23:
                    return;
                case >= 21:
                    home.Announce(new Announcement("speaker.bedroom", "Remember to set alarm", home.CurrentTime));
                    break;
            }
        });
    }
}