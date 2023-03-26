using IntegrationTestsWithBDD;
using IntegrationTestWithBDD.ApplicationService;
using IntegrationTestWithBDD.Specflow.MockService;
using IntegrationTestWithBDD.Specflow.TestServer;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace IntegrationTestWithBDD.Specflow.Steps;

[Binding]
public class AppendTextStep
{
    private readonly ScenarioContext _scenarioContext;

    public AppendTextStep(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }
    
    [Given(@"a text ""(.*)""")]
    public void GivenAText(string p0)
    {
        _scenarioContext.Set(p0,RequestResponseKey.AppendTextName);
    }

    [When(@"append the text")]
    public async Task WhenAppendTheText()
    {
        if (! _scenarioContext.TryGetValue<string>(RequestResponseKey.AppendTextName,out var text))
            text = string.Empty;
        
        var services = _scenarioContext.Get<ServiceCollection>();
        
        var serverFixture = new TestServer.TestServer(services);
        
        var textGrpc = new Text.TextClient(serverFixture.GrpcChannel);

        var req = new AppendTextRequest
        {
            Text = text
        };
        
        var textReply = await textGrpc.AppendTextAsync(req);

        _scenarioContext.Set(textReply);
    }

    [Then(@"the result text should be ""(.*)""")]
    public void ThenTheResultTextShouldBe(string p0)
    {
        var reply = _scenarioContext.Get<AppendTextReply>();
        
        Assert.Equal(p0,reply.Message);
    }

    [Given(@"using mock append text service")]
    public void GivenUsingMockAppendTextService()
    {
        var collection = _scenarioContext.Get<ServiceCollection>();
        // override the IAppendTextService in Program.cs
        collection.AddSingleton<IAppendTextService, MockAppendTextService>();
    }
}