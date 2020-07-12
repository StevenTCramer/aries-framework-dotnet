namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using System;
  using System.Linq;
  using System.Net.Http;
  using System.Threading.Tasks;

  [NotTest]
  public partial class TestApplication : IDisposable, IAsyncDisposable
  {
    private readonly Application Application;
    private readonly MediationTestService MediationTestService;
    private bool Disposed;
    public HttpClient HttpClient { get; }
    public IServiceProvider ServiceProvider { get; }
    public WebApiTestService WebApiTestService { get; }

    public TestApplication(string aEnvironment, string[] aUrls)
    {
      Application = new Application(aEnvironment, aUrls);
      ServiceProvider = Application.Host.Services;
      MediationTestService = new MediationTestService(ServiceProvider);
      HttpClient = new HttpClient
      {
        BaseAddress = new Uri(aUrls.First())
      };
      WebApiTestService = new WebApiTestService(HttpClient);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
      Console.WriteLine("==== TestApplication.DisposeAsync ====");
      await DisposeAsyncCore();
      Dispose(false);
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
  }
}
