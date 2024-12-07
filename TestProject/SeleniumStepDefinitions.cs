using BddAndSpecFlow.Internals;
using MyHomeAutomationSystem.Internals;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow.Assist;

namespace BddAndSpecFlow;

[Binding]
public sealed class SeleniumStepDefinitions
{
    private readonly ScenarioContext       _scenarioContext;
    private          AutomationTestContext _ctx;

    public SeleniumStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;

    }

    [BeforeScenario("UI")]
    public void Init()
    {
        var options = new ChromeOptions();
        options.AddArgument("--window-position=0,0");
        var driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        _scenarioContext["chromeDriver"]        = driver;
    }

    [AfterScenario("UI")]
    public void AfterScenario()
    {
        var driver = (ChromeDriver)_scenarioContext["chromeDriver"];
        driver.Close();
    }

    [Given(@"the user is on login page")]
    public async Task GivenTheUserIsOnLoginPage()
    {
        var driver = (ChromeDriver)_scenarioContext["chromeDriver"];
        driver.Url = "http://192.168.1.3:8123/dashboard-pluralsight/0";
        await Task.Delay(1000);
    }

    [When(@"the user logs in with valid credentials")]
    public async Task WhenTheUserEntersValidCredentials()
    {
        var driver   = (ChromeDriver)_scenarioContext["chromeDriver"];
        
        var userName = driver.FindElement(By.Name("username"));
        userName.SendKeys("pluralsight");
        await Task.Delay(500);

        var password = driver.FindElement(By.Name("password"));
        password.SendKeys("pluralsight");
        await Task.Delay(500);

        password.SendKeys(Keys.Enter);
    }

    [When(@"the user logs in with invalid credentials")]
    public async Task WhenTheUserEntersInvalidCredentials()
    {
        var driver = (ChromeDriver)_scenarioContext["chromeDriver"];

        var userName = driver.FindElement(By.Name("username"));
        userName.SendKeys("wrong_username");
        await Task.Delay(500);

        var password = driver.FindElement(By.Name("password"));
        password.SendKeys("wrong_password");
        await Task.Delay(500);

        password.SendKeys(Keys.Enter);
    }

    [Then(@"the home automation dashboard is visible")]
    public async Task ThenTheHomeAutomationDashboardIsVisible()
    {
        var driver         = (ChromeDriver)_scenarioContext["chromeDriver"];
        driver.FindElement(By.XPath("//home-assistant"));
        await Task.Delay(1000);
    }

    [Then(@"the home automation system throws an exception")]
    public async Task ThenTheHomeAutomationThrows()
    {
        var driver = (ChromeDriver)_scenarioContext["chromeDriver"];
        Assert.Throws<NoSuchElementException>( () => driver.FindElement(By.XPath("//home-assistant")));
        await Task.Delay(1000);
    }
}