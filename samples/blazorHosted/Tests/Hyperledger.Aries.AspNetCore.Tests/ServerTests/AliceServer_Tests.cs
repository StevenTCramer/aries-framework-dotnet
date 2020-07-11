namespace AliceServer_
{
  using Hyperledger.Aries.AspNetCore.Configuration;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using Microsoft.Extensions.Options;
  using Microsoft.Extensions.DependencyInjection;
  using FluentAssertions;
  using System.Threading.Tasks;

  public class AliceServer_Tests
  {
    private readonly AgentSettings AgentSettings;
    private readonly AliceApplication AliceServer;
    private readonly FaberApplication FaberServer;

    public AliceServer_Tests(AliceApplication aAliceServer, FaberApplication aFaberServer)
    {
      AliceServer = aAliceServer;
      FaberServer = aFaberServer;
      AgentSettings = aAliceServer.ServiceProvider.GetService<IOptions<AgentSettings>>().Value;
    }

    public void Be_Valid()
    {
      AgentSettings.AgentName.Should().Be("Alice");
    }

    //public async Task StopServer()
    //{
    //  await AliceServer.WebHost.StopAsync();
    //  await FaberServer.WebHost.StopAsync();
    //}
  }
}
