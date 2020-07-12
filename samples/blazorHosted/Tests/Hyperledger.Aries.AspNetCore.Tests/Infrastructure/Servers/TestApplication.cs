namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using MediatR;
  using Microsoft.Extensions.Hosting;
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;

  public class TestApplication : IDisposable
  {
    private readonly Application Application;
    private readonly MediationTestService MediationTestService;
    private readonly WebApiTestService WebApiTestService;
    public IServiceProvider ServiceProvider { get; }

    public TestApplication(string aEnvironment)
    {
      Application = new Application(aEnvironment);
      ServiceProvider = Application.Host.Services;
      MediationTestService = new MediationTestService(ServiceProvider);
      var httpClient = new HttpClient();
      WebApiTestService = new WebApiTestService(httpClient);
    }

    internal Task Send(IRequest aRequest) => MediationTestService.Send(aRequest);

    internal Task<TResponse> Send<TResponse>(IRequest<TResponse> aRequest) =>
      MediationTestService.Send(aRequest);

    public void Dispose()
    {
      Application.Dispose();
    }
  }
}
