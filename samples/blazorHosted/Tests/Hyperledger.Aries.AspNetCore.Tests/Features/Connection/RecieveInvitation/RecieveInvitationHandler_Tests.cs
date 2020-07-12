namespace ReceiveInvitationHandler
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly AliceApplication AliceApplication;
    private readonly ReceiveInvitationRequest ReceiveInvitationRequest;

    public Handle_Returns(AliceApplication aAliceApplication)
    {
      AliceApplication = aAliceApplication;
      ReceiveInvitationRequest = TestApplication.CreateValidReceiveInvitationRequest();
    }

    public async Task ReceiveInvitationResponse()
    {
      ReceiveInvitationResponse receiveInvitationResponse = await AliceApplication.Send(ReceiveInvitationRequest);

      TestApplication.ValidateReceiveInvitationResponse(ReceiveInvitationRequest, receiveInvitationResponse);
    }

    public async Task Setup() => await AliceApplication.ResetAgent();
  }
}
