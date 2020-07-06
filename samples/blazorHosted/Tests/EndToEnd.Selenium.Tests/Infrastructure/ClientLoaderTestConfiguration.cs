namespace Hyperledger.Aries.AspNetCore.EndToEnd.Tests.Infrastructure
{
  using System;
  using Hyperledger.Aries.AspNetCore.Features.ClientLoaders;

  public class TestClientLoaderConfiguration : IClientLoaderConfiguration
  {
    public TimeSpan DelayTimeSpan => TimeSpan.FromMilliseconds(10);
  }
}
