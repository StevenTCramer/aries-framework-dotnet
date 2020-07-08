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
    private readonly AliceServer AliceServer;
    private readonly FaberServer FaberServer;

    public AliceServer_Tests(AliceServer aAliceServer, FaberServer aFaberServer)
    {
      AliceServer = aAliceServer;
      FaberServer = aFaberServer;
      AgentSettings = aAliceServer.WebHost.Services.GetService<IOptions<AgentSettings>>().Value;
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
