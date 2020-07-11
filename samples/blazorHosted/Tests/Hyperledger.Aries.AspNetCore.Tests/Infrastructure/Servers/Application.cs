namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  [NotTest]
  public class Application : IAsyncDisposable, IDisposable
  {
    private readonly CancellationTokenSource CancellationTokenSource;
    private readonly IHostBuilder HostBuilder;
    public IHost Host { get; }

    public Application(string aEnvironmentName)
    {
      CancellationTokenSource = new CancellationTokenSource();

      HostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults
        (
          aWebHostBuilder =>
          {
            aWebHostBuilder.UseStaticWebAssets();
            aWebHostBuilder.UseStartup<Startup>();
            aWebHostBuilder.UseEnvironment(aEnvironmentName);
            aWebHostBuilder.UseShutdownTimeout(TimeSpan.FromSeconds(30));
          }
        );

      Host = HostBuilder.Build();
      Host.StartAsync(CancellationTokenSource.Token);
    }

    public void Dispose()
    {
      Console.WriteLine("==== Server.Dispose ====");
      CancellationTokenSource.Cancel();
    }

    public async ValueTask DisposeAsync()
    {
      Console.WriteLine("==== Server.DisposeAsync ====");
      await Host.StopAsync(CancellationTokenSource.Token);
      CancellationTokenSource.Cancel();
    }

    public void KillIt()
    {
      IHostApplicationLifetime hostApplicationLifetime = Host.Services.GetService<IHostApplicationLifetime>();
      hostApplicationLifetime.StopApplication();
    }
  }
}
