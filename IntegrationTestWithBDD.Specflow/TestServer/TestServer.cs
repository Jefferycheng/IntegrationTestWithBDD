using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTestWithBDD.Specflow.TestServer;

public class TestServer
{
    public readonly CustomWebApplicationFactory<Program> Factory;
    public GrpcChannel GrpcChannel { get; private set; }
    public IServiceProvider Services { get; private set; }

    public TestServer(IServiceCollection additionalServices)
    {
        Factory = new CustomWebApplicationFactory<Program>(additionalServices);
        
        var client = Factory.CreateDefaultClient();
        
        GrpcChannel = GrpcChannel.ForAddress(client.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = client
        });
        
        Services = Factory.Services;
    }
        
    public void Dispose()
    {
        Factory.Dispose();
    }
}

public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    private readonly IServiceCollection _mockServices;

    public CustomWebApplicationFactory(IServiceCollection mockServices)
    {
        _mockServices = mockServices;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Add(_mockServices);
        });
    }
}