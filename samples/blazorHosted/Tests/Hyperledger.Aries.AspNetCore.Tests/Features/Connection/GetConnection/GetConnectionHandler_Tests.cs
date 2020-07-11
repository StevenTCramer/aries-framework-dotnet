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

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerSettings aJsonSerializerSettings
    ) : base(aWebApplicationFactory, aJsonSerializerSettings)
    {
      GetConnectionRequest = CreateValidGetConnectionRequest();
    }

    public async Task GetConnectionResponse()
    {
      // Arrange
      CreateInvitationResponse createInvitationResponse = await CreateAnInvitation();
      GetConnectionRequest.ConnectionId = createInvitationResponse.ConnectionRecord.Id;

      //Act
      GetConnectionResponse GetConnectionResponse = await Send(GetConnectionRequest);

      //Assert
      ValidateGetConnectionResponse(GetConnectionRequest, GetConnectionResponse);
    }

    public async Task Setup() => await ResetAgent();
  }
}
