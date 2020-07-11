namespace FaberServer_
{
  using Hyperledger.Aries.AspNetCore.Configuration;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using Microsoft.Extensions.Options;
  using Microsoft.Extensions.DependencyInjection;
  using FluentAssertions;

  public class FaberServer_Tests
  {
    private readonly AgentSettings AgentSettings;

    public FaberServer_Tests(FaberApplication aFaberServer)
    {
      AgentSettings = aFaberServer.ServiceProvider.GetService<IOptions<AgentSettings>>().Value;
    }

    public void Be_Valid()
    {
      AgentSettings.AgentName.Should().Be("Faber");
    }
  }
}
