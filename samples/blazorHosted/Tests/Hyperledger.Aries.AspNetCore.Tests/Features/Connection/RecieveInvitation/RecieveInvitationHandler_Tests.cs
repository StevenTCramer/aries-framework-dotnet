namespace RecieveInvitationHandler
{
  using System.Threading.Tasks;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using FluentAssertions;
  using Newtonsoft.Json;
  using Hyperledger.Aries.Features.DidExchange;

  public class Handle_Returns : BaseTest
  {
    private readonly RecieveInvitationRequest RecieveInvitationRequest;

    public Handle_Returns
    (
      AliceWebApplicationFactory aAliceWebApplicationFactory,
      JsonSerializerSettings aJsonSerializerSettings
    ) : base(aAliceWebApplicationFactory, aJsonSerializerSettings)
    {
      RecieveInvitationRequest = CreateValidRecieveInvitationRequest();
    }

    public async Task RecieveInvitationResponse()
    {
      RecieveInvitationResponse recieveInvitationResponse = await Send(RecieveInvitationRequest);

      ValidateRecieveInvitationResponse(RecieveInvitationRequest, recieveInvitationResponse);
    }

    public async Task Setup()
    {
      await ResetAgent();
    }
  }
}