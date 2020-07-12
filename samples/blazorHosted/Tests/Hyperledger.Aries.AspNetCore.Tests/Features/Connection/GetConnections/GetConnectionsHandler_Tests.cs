namespace GetConnectionsHandler
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;
    private readonly GetConnectionsRequest GetConnectionsRequest;

    public Handle_Returns(FaberApplication aFaberApplication)
    {
      GetConnectionsRequest = TestApplication.CreateValidGetConnectionsRequest();
      FaberApplication = aFaberApplication;
    }

    public async Task GetConnectionsResponse()
    {
      // Arrage
      await FaberApplication.CreateAnInvitation();

      // Act
      GetConnectionsResponse getConnectionsResponse = await FaberApplication.Send(GetConnectionsRequest);

      // Assert
      TestApplication.ValidateGetConnectionsResponse(GetConnectionsRequest, getConnectionsResponse);
    }

    public async Task Setup() => await FaberApplication.ResetAgent();
  }
}
