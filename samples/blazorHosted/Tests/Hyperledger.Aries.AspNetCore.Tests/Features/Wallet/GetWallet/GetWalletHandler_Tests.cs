namespace GetWalletHandler
{
  using Hyperledger.Aries.AspNetCore.Features.Wallets;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;
    private readonly GetWalletRequest GetWalletRequest;

    public Handle_Returns(FaberApplication aFaberApplication)
    {
      FaberApplication = aFaberApplication;
      GetWalletRequest = new GetWalletRequest();
    }

    public async Task GetWalletResponse()
    {
      GetWalletResponse getWalletResponse = await FaberApplication.Send(GetWalletRequest);

      TestApplication.ValidateGetWalletResponse(GetWalletRequest, getWalletResponse);
    }

    public async Task Setup() => await FaberApplication.ResetAgent();
  }
}
