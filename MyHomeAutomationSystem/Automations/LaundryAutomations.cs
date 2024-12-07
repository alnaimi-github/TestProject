using System.Reactive.Linq;
using MyHomeAutomationSystem.Internals;

namespace MyHomeAutomationSystem.Automations;

public sealed class LaundryAutomations
{
    public LaundryAutomations(MyHome home)
    {
        home.StateChanges.Where(s => s.Name == "motion.laundry").Subscribe(_ =>
        {
            if (home.CurrentTime.Hour < 7)
                return;

            home.Announce(new Announcement("speaker.laundry", "Dryer is ready", home.CurrentTime));
        });
    }
}