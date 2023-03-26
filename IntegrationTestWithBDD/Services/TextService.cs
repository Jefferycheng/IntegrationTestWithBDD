using Grpc.Core;
using IntegrationTestsWithBDD;
using IntegrationTestWithBDD.ApplicationService;

namespace IntegrationTestWithBDD.Services;

public class TextService : Text.TextBase
{
    private readonly IAppendTextService _appendTextService;

    public TextService(IAppendTextService appendTextService)
    {
        _appendTextService = appendTextService;
    }
    
    public override Task<AppendTextReply> AppendText(AppendTextRequest request,
        ServerCallContext context)
    {
        return Task.FromResult(new AppendTextReply
        {
            Message = _appendTextService.AppendText(request.Text)
        });
    }
}