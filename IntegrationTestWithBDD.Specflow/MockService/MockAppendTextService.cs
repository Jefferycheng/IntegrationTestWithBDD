using IntegrationTestWithBDD.ApplicationService;

namespace IntegrationTestWithBDD.Specflow.MockService;

public class MockAppendTextService : IAppendTextService
{
    public string AppendText(string txt) => txt + " this function has been change to mock";
}