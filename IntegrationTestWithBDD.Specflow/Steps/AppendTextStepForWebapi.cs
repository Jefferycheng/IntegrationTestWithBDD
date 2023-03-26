using IntegrationTestWithBDD.Specflow.TestServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace IntegrationTestWithBDD.Specflow.Steps;

[Binding]
public class AppendTextStepForWebapi
{
    private readonly ScenarioContext _scenarioContext;

    public AppendTextStepForWebapi(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"api- a text ""(.*)""")]
    public void GivenApiAText(string p0)
    {
        _scenarioContext.Set(p0, RequestResponseKey.AppendTextName);
    }

    [When(@"api- append the text")]
    public async Task WhenApiAppendTheText()
    {
        if (! _scenarioContext.TryGetValue<string>(RequestResponseKey.AppendTextName,out var text))
            text = string.Empty;
        
        var factory = new CustomWebApplicationFactory<Program>(new ServiceCollection());

        var client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost:8080")
        });
        
        var response = await client.GetStringAsync($"api/AppendText/append?text={text}");
        
        _scenarioContext.Set(response,RequestResponseKey.AppendTextReturnMsg);
    }

    [Then(@"api- the result text should be ""(.*)""")]
    public void ThenApiTheResultTextShouldBe(string p0)
    {
        var returnMsg = _scenarioContext.Get<string>(RequestResponseKey.AppendTextReturnMsg);
        
        Assert.Equal(p0,returnMsg);
    }
}