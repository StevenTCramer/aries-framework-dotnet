namespace CreateInvitationEndpoint
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly CreateInvitationRequest CreateInvitationRequest;
    private readonly FaberApplication FaberApplication;

    public Returns(FaberApplication aFaberApplication)
    {
      CreateInvitationRequest = TestApplication.CreateValidCreateInvitationRequest();
      FaberApplication = aFaberApplication;
    }

    public async Task CreateInvitationResponse_using_Json_Net()
    {
      CreateInvitationResponse createInvitationResponse =
        await FaberApplication.WebApiTestService.Post(CreateInvitationRequest.GetRoute(), CreateInvitationRequest);

      TestApplication.ValidateCreateInvitationResponse(CreateInvitationRequest, createInvitationResponse);
    }

    public async Task CreateInvitationResponse_using_System_Text_Json()
    {
      HttpResponseMessage httpResponseMessage =
        await FaberApplication.HttpClient
          .PostAsJsonAsync<CreateInvitationRequest>(CreateInvitationRequest.GetRoute(), CreateInvitationRequest);

      CreateInvitationResponse createInvitationResponse =
        await httpResponseMessage.Content.ReadFromJsonAsync<CreateInvitationResponse>();

      TestApplication.ValidateCreateInvitationResponse(CreateInvitationRequest, createInvitationResponse);
    }

    public async Task ValidationError()
    {
      // Set invalid value.
      // This is NOT to test all validation rules just to test that they are wired up.
      // If one fires they should all fire and each is tested in the Validator test
      CreateInvitationRequest.InviteConfiguration = null;

      await FaberApplication.WebApiTestService.ConfirmEndpointValidationError
      (
        CreateInvitationRequest.GetRoute(),
        CreateInvitationRequest,
        nameof(CreateInvitationRequest.InviteConfiguration)
      );
    }
  }
}
