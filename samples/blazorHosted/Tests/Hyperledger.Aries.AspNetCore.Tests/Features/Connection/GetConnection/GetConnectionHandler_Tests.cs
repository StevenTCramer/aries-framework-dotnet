namespace GetConnectionHandler_
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Newtonsoft.Json;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly GetConnectionRequest GetConnectionRequest;
    private readonly FaberApplication FaberApplication;

    public Handle_Returns
    (
      FaberApplication aFaberApplication
    ) 
    {
      GetConnectionRequest = TestApplication.CreateValidGetConnectionRequest();
      FaberApplication = aFaberApplication;
    }

    public async Task GetConnectionResponse()
    {
      // Arrange
      CreateInvitationResponse createInvitationResponse = await FaberApplication.CreateAnInvitation();
      GetConnectionRequest.ConnectionId = createInvitationResponse.ConnectionRecord.Id;

      //Act
      GetConnectionResponse getConnectionResponse = await FaberApplication.Send(GetConnectionRequest);

      //Assert
      TestApplication.ValidateGetConnectionResponse(GetConnectionRequest, getConnectionResponse);
    }

    public async Task Setup() => await FaberApplication.ResetAgent();
  }
}
