namespace GetCredentialsHandler
{
  using Hyperledger.Aries.AspNetCore.Features.Credentials;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Threading.Tasks;

  public class Handle_Returns
  {
    private readonly FaberApplication FaberApplication;
    private readonly GetCredentialsRequest GetCredentialsRequest;

    public Handle_Returns(FaberApplication aFaberApplication)
    {
      GetCredentialsRequest = new GetCredentialsRequest();
      FaberApplication = aFaberApplication;
    }

    public async Task GetCredentialsResponse()
    {
      GetCredentialsResponse getCredentialsResponse = await FaberApplication.Send(GetCredentialsRequest);

      TestApplication.ValidateGetCredentialsResponse(GetCredentialsRequest, getCredentialsResponse);
    }
  }
}
