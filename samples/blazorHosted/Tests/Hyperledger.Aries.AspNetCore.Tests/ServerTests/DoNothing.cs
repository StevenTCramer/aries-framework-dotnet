namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.ServerTests
{
  using FluentAssertions;
  public class DoNothing
  {
    public void JunkTest()
    {
      5.Should().Be(5);
    }
  }
}
