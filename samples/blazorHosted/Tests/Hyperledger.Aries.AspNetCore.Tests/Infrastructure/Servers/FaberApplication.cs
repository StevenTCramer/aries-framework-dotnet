namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  [NotTest]
  public class FaberApplication : TestApplication
  {
    public FaberApplication() :
      base
      (
        aEnvironment: "Development",
        aUrls: new[]
        {
          "https://localhost:5551",
          "http://localhost:5550"
        }
      ) { }
  }
}
