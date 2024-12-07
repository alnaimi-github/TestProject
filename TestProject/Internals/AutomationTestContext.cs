using Microsoft.Reactive.Testing;
using MyHomeAutomationSystem.Automations;
using MyHomeAutomationSystem.Internals;

namespace BddAndSpecFlow.Internals;

public class AutomationTestContext
{
    
    private readonly EntityStates<IEntity> _stateChanges = new();
    public           TestScheduler         Scheduler     = new();

    public AutomationTestContext()
    {
        Home = new MyHome(_stateChanges, Scheduler);
        _    = new KitchenAutomations(Home);
        _    = new LaundryAutomations(Home);
        _    = new BedroomAutomations(Home);
    }

    public MyHome Home { get; }

    public void SetTime(string datetime)
    {
        Scheduler.AdvanceTo(DateTime.Parse(datetime).Ticks);
        //Console.WriteLine($"--> Now:{Scheduler.Now.DateTime:G}");
    }

    public void TriggerState(IEntity entity) => _stateChanges.SetSensorValue(entity);
}