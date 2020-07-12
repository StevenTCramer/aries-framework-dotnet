namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using FluentAssertions;

  public partial class TestApplication
  {
    internal static GetConnectionsRequest CreateValidGetConnectionsRequest() => new GetConnectionsRequest();

    internal static void ValidateGetConnectionsResponse(GetConnectionsRequest aGetConnectionsRequest, GetConnectionsResponse aGetConnectionsResponse)
    {
      aGetConnectionsResponse.CorrelationId.Should().Be(aGetConnectionsRequest.CorrelationId);
      aGetConnectionsResponse.ConnectionRecords.Should().NotBeNull();
      aGetConnectionsResponse.ConnectionRecords.Count.Should().Be(1);
    }
  }
}
