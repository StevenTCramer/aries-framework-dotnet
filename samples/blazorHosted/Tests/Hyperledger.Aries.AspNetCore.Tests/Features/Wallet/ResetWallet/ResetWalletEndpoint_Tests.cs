namespace ResetWalletEndpoint
{
  using Hyperledger.Aries.AspNetCore.Features.Wallets;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;
    private readonly ResetWalletRequest ResetWalletRequest;

    public Returns(FaberApplication aFaberApplication)
    {
      ResetWalletRequest = new ResetWalletRequest();
      FaberApplication = aFaberApplication;
    }

    public async Task ResetWalletResponse_using_Json_Net()
    {
      ResetWalletResponse resetWalletResponse =
        await FaberApplication.WebApiTestService.Post<ResetWalletResponse>(ResetWalletRequest.GetRoute(), ResetWalletRequest);

      TestApplication.ValidateResetWalletResponse(ResetWalletRequest, resetWalletResponse);
    }

    public async Task ResetWalletResponse_using_System_Text_Json()
    {
      HttpResponseMessage httpResponseMessage =
        await FaberApplication.HttpClient.PostAsJsonAsync<ResetWalletRequest>(ResetWalletRequest.GetRoute(), ResetWalletRequest);

      ResetWalletResponse resetWalletResponse =
        await httpResponseMessage.Content.ReadFromJsonAsync<ResetWalletResponse>();

      TestApplication.ValidateResetWalletResponse(ResetWalletRequest, resetWalletResponse);
    }

    // There are no validation requirements for this request
    public void ValidationError() { }
  }
}
