namespace GetWalletEndpoint
{
  using Hyperledger.Aries.AspNetCore.Features.Wallets;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;
    private readonly GetWalletRequest GetWalletRequest;

    public Returns(FaberApplication aFaberApplication)
    {
      FaberApplication = aFaberApplication;
      GetWalletRequest = new GetWalletRequest();
    }

    public async Task GetWalletResponse_using_Json_Net()
    {
      GetWalletResponse getWalletResponse =
        await FaberApplication.WebApiTestService.GetJsonAsync<GetWalletResponse>(GetWalletRequest.GetRoute());

      TestApplication.ValidateGetWalletResponse(GetWalletRequest, getWalletResponse);
    }

    public async Task GetWalletResponse_using_System_Text_Json()
    {
      GetWalletResponse getWalletResponse =
        await FaberApplication.HttpClient.GetFromJsonAsync<GetWalletResponse>(GetWalletRequest.GetRoute());

      TestApplication.ValidateGetWalletResponse(GetWalletRequest, getWalletResponse);
    }

    // There are no validation requirements for this request
    public void ValidationError() { }
  }
}
