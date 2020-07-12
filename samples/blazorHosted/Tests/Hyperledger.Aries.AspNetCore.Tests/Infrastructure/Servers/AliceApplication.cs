namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  [NotTest]
  public class AliceApplication : TestApplication
  {
    public AliceApplication() :
      base
      (
        "Alice",
        new[]
        {
          "https://localhost:5553",
          "http://localhost:5552"
        }
      ) { }
  }
}
