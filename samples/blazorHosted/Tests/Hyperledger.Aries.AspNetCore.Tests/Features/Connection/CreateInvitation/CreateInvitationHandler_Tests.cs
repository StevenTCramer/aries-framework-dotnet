namespace CreateInvitationHandler
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly CreateInvitationRequest CreateInvitationRequest;
    private readonly FaberApplication FaberApplication;

    public Handle_Returns
    (
      FaberApplication aFaberApplication
    )
    {
      FaberApplication = aFaberApplication;
      CreateInvitationRequest = TestApplication.CreateValidCreateInvitationRequest();
    }

    public async Task CreateInvitationResponse()
    {
      CreateInvitationResponse createInvitationResponse = await FaberApplication.Send(CreateInvitationRequest);

      TestApplication.ValidateCreateInvitationResponse(CreateInvitationRequest, createInvitationResponse);
    }
  }
}
