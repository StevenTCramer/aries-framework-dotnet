namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  [NotTest]
  public class Server : IAsyncDisposable, IDisposable
  {
    private readonly IWebHostBuilder WebHostBuilder;
    private readonly CancellationTokenSource CancellationTokenSource;

    public IWebHost WebHost { get; }

    public Server(string aEnvironmentName)
    {
      CancellationTokenSource = new CancellationTokenSource();

      WebHostBuilder = Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults
        (
          aWebHostBuilder =>
          {
            aWebHostBuilder.UseStaticWebAssets();
            aWebHostBuilder.UseStartup<Startup>();
            aWebHostBuilder.UseEnvironment(aEnvironmentName);
          }
        ) as IWebHostBuilder;

      WebHost = WebHostBuilder.Build();
      WebHost.StartAsync(CancellationTokenSource.Token);
    }

    public ValueTask DisposeAsync()
    {
      WebHost.StopAsync(CancellationTokenSource.Token);
      return default;
    }

    public void Dispose()
    {
      CancellationTokenSource.Cancel();
    }
  }
}
