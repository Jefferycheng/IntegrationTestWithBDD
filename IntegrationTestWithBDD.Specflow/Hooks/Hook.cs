using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace IntegrationTestWithBDD.Specflow.Hooks;

[Binding]
public class Hook
{
    /// <summary>
    /// Before scenario given a empty service collection to mock service 
    /// </summary>
    [BeforeScenario]
    public static void BeforeScenario(ScenarioContext scenarioContext)
    {
        scenarioContext.Set(new ServiceCollection());
    }
}