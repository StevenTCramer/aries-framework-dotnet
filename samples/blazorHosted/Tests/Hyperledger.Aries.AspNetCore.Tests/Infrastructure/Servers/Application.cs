namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  [NotTest]
  public class Application : IDisposable
  {
    private bool Disposed;
    private readonly IHostBuilder HostBuilder;
    public IHost Host { get; }

    public Application(string aEnvironmentName)
    {
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
      Host.StartAsync();
    }

    protected virtual void Dispose(bool aIsDisposing)
    {
      if (Disposed) return;

      if (aIsDisposing)
      {
        Console.WriteLine("==== Wait till Host Stops ====");
        Host?.StopAsync().GetAwaiter().GetResult();
        Console.WriteLine("==== Now dispose of Host ====");
        Host?.Dispose();
      }

      Disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
