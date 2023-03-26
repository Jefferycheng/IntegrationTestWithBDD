namespace IntegrationTestWithBDD.ApplicationService;

public class AppendTextService : IAppendTextService
{
    public string AppendText(string txt) => txt + " this is appended text";
}