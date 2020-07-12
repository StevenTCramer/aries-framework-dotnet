namespace DeleteConnectionHandler
{
  using FluentAssertions;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;

    private CreateInvitationResponse CreateInvitationResponse { get; set; }
    private DeleteConnectionRequest DeleteConnectionRequest { get; set; }

    public Handle_Returns(FaberApplication aFaberApplication)
    {
      DeleteConnectionRequest = new DeleteConnectionRequest("ConnectionId");
      FaberApplication = aFaberApplication;
    }

    public async Task DeleteConnectionResponse()
    {
      DeleteConnectionRequest.ConnectionId = CreateInvitationResponse.ConnectionRecord.Id;

      DeleteConnectionResponse deleteConnectionResponse = await FaberApplication.Send(DeleteConnectionRequest);

      TestApplication.ValidateDeleteConnectionResponse(DeleteConnectionRequest, deleteConnectionResponse);

      //Confirm it is deleted

      GetConnectionResponse getConnectionResponse = await FaberApplication.Send(new GetConnectionRequest(CreateInvitationResponse.ConnectionRecord.Id));
      getConnectionResponse.ConnectionRecord.Should().BeNull();
    }

    public async Task Setup()
    {
      await FaberApplication.ResetAgent();
      CreateInvitationResponse = await FaberApplication.CreateAnInvitation();
    }
  }
}
