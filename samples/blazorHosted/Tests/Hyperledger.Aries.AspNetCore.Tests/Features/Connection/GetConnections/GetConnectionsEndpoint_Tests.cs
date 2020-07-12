namespace GetConnectionsEndpoint
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;
    private readonly GetConnectionsRequest GetConnectionsRequest;

    public Returns(FaberApplication aFaberApplication)
    {
      GetConnectionsRequest = new GetConnectionsRequest();
      FaberApplication = aFaberApplication;
    }

    public async Task GetConnectionsResponse_using_Json_Net()
    {
      GetConnectionsResponse getConnectionsResponse =
        await FaberApplication.WebApiTestService.GetJsonAsync<GetConnectionsResponse>(GetConnectionsRequest.GetRoute());

      TestApplication.ValidateGetConnectionsResponse(GetConnectionsRequest, getConnectionsResponse);
    }

    public async Task GetConnectionsResponse_using_System_Text_Json()
    {
      GetConnectionsResponse getConnectionsResponse =
        await FaberApplication.HttpClient.GetFromJsonAsync<GetConnectionsResponse>(GetConnectionsRequest.GetRoute());

      TestApplication.ValidateGetConnectionsResponse(GetConnectionsRequest, getConnectionsResponse);
    }

    public async Task Setup()
    {
      await FaberApplication.ResetAgent();
      await FaberApplication.CreateAnInvitation();
    }

    // There are no validation requirements for this request
    public void ValidationError() { }
  }
}
