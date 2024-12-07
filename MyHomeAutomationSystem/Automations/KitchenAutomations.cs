using System.Reactive.Linq;
using MyHomeAutomationSystem.Internals;

namespace MyHomeAutomationSystem.Automations;

public sealed class KitchenAutomations
{
    public KitchenAutomations(MyHome home)
    {
        home.StateChanges.Where(s=>s.Name == "motion.kitchen").Subscribe(_ =>
        {
            if (home.Entities.BinarySensors("binary.dishwasher").IsOff)
                return;
            if (home.Entities.BinarySensors("binary.dishwasher_unloaded").IsOn)
                return;
            if (( home.CurrentTime - home.LastPrompt("speaker.kitchen").Sent ).TotalMinutes <= 60)
                return;
            if (home.CurrentTime.Hour < 7)
                return;

            home.Prompt(new Prompt("speaker.kitchen", "Dishwasher is done, have you unloaded it?", home.CurrentTime));
        });
    }
}