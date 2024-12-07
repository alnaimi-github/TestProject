using BddAndSpecFlow.Internals;
using MyHomeAutomationSystem.Internals;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow.Assist;

namespace BddAndSpecFlow;

[Binding]
public sealed class StepDefinitions
{
    private readonly ScenarioContext       _scenarioContext;
    private          AutomationTestContext _ctx;

    public StepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario("Automations")]
    public void Init()
    {
        _ctx = new AutomationTestContext();
    }

    [Given(@"it is before 5 AM")]
    public void GivenItIsBefore5Am()
    {
        _ctx.SetTime("2023-04-06 04:59:59");
    }

    [Then(@"there is no announcement")]
    public void ThenThereIsNoAnnouncement()
    {
        _ctx.Home.Announcements.Should().NotContain(e => e.SpeakerName == "speaker.kitchen");
    }

    [When(@"there is motion in the (.*)")]
    public void WhenThereIsMotionInTheRoom(MotionSensorWithMotion sensor)
    {
        _ctx.TriggerState(sensor);
    }  

    [Given(@"there is no motion in the (.*)")]
    public void GivenThereIsNoMotionInTheRoom(string roomName)
    {
        _ctx.TriggerState(new MotionSensor($"motion.{roomName}", false));
    }

    [Given(@"the dishwasher is complete")]
    public void GivenTheDishwasherIsComplete()
    {
        _ctx.TriggerState(new BinarySensor("binary.dishwasher", true));
    }
    [Given(@"I have not acknowledged unloading it")]
    public void GivenIHaveNotAcknowledgedUnloadingIt()
    {
        _ctx.TriggerState(new BinarySensor("binary.dishwasher_unloaded", false));
    }
    [Given(@"the last reminder was more than 60 minutes ago")]
    public void GivenTheLastReminderWasMoreThan60MinutesAgo()
    {
        _ctx.Home.Prompt(new Prompt("speaker.kitchen", "Dishwasher is done", _ctx.Scheduler.Now.DateTime));
    }
    [Given(@"it is after 7 AM")]
    public void GivenItIsAfter7Am()
    {
        _ctx.SetTime("2023-04-06 07:00:01");
    }
    [Then(@"my smart speaker announces it's done and prompts me if it has been unloaded and stores the acknowledgement")]
    public void ThenMySmartSpeakerAnnouncesItSDoneAndPromptsMeIfItHasBeenUnloadedAndStoresTheAcknowledgement()
    {
        _ctx.Home.LastPrompt("speaker.kitchen").Message.Should().Be("Dishwasher is done, have you unloaded it?");
    }


    public class MotionTestConfig
    {
        public string RoomName { get; set; }
        public string TimeOfDay { get; set; }
    }

    public enum AnnouncementType
    {
        Prompted,
        Announced
    }

    [Given(@"the following config")]
    public void GivenTheFollowingConfig(Table table)
    {
        _scenarioContext["configs"] = table.CreateSet<MotionTestConfig>();
    }

    [When(@"there is motion in room at specified time")]
    public void WhenThereIsMotionInRoomAtSpecifiedTime()
    {
        foreach (var motionTestConfig in (IEnumerable<MotionTestConfig>)_scenarioContext["configs"])
        {
            var stepInfoStepDefinitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;
            var stepInfoStepText           = _scenarioContext.StepContext.StepInfo.Text;
            var motionSensor               = new MotionSensorWithMotion(motionTestConfig.RoomName);

            Console.WriteLine($"{stepInfoStepDefinitionType} {stepInfoStepText}: Set Time to {motionTestConfig.TimeOfDay}");
            _ctx.SetTime(motionTestConfig.TimeOfDay);

            Console.WriteLine($"{stepInfoStepDefinitionType} {stepInfoStepText}: Trigger motion for {motionSensor.Name}");
            _ctx.TriggerState(motionSensor);
        }
    }

    [Then(@"my smart speaker (.*)")]
    public void ThenMySmartSpeakerPrompted(AnnouncementType type, Table table)
    {
        switch (type)
        {
            case AnnouncementType.Prompted:
                table.CompareToSet(_ctx.Home.Prompts);
                break;
            case AnnouncementType.Announced:
                table.CompareToSet(_ctx.Home.Announcements);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}