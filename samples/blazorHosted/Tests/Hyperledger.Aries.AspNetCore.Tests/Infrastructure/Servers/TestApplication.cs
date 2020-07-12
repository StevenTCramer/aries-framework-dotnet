namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using MediatR;
  using Microsoft.Extensions.Hosting;
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;

  public class TestApplication : IDisposable, IAsyncDisposable
  {
    private bool Disposed;
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
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool aIsDisposing)
    {
      if (Disposed) return;

      if (aIsDisposing)
      {
        Application?.Dispose();
      }

      Disposed = true;
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
      Console.WriteLine("==== TestApplication.DisposeAsyncCore ====");
      await Application.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
      Console.WriteLine("==== TestApplication.DisposeAsync ====");
      await DisposeAsyncCore();
      Dispose(false);
      GC.SuppressFinalize(this);
    }
  }
}
