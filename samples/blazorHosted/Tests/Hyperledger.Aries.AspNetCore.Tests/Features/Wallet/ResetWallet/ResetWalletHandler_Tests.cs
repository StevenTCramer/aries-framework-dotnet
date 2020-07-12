namespace ResetWalletHandler
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Features.Wallets;
  using Hyperledger.Aries.AspNetCore.Server;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Newtonsoft.Json;
  using System.Threading.Tasks;

  public class Handle_Returns : BaseTest
  {
    private readonly ResetWalletRequest ResetWalletRequest;
    private readonly FaberApplication FaberApplication;

    public Handle_Returns(FaberApplication aFaberApplication)
    {
      FaberApplication = aFaberApplication;
      ResetWalletRequest = new ResetWalletRequest();
    }

    public async Task ResetWalletResponse_and_reset_wallet()
    {
      // Arrage
      // Add something to the wallet 
      await FaberApplication.CreateAnInvitation();

      //Act
      ResetWalletResponse resetWalletResponse = await FaberApplication.Send(ResetWalletRequest);

      // See what is in the wallet
      GetConnectionsRequest getConnectionsRequest = TestApplication.CreateValidGetConnectionsRequest();
      GetConnectionsResponse getConnectionsResponse = await FaberApplication.Send(getConnectionsRequest);

      // Assert Item created isn't there
      getConnectionsResponse.ConnectionRecords.Count.Should().Be(0);

      TestApplication.ValidateResetWalletResponse(ResetWalletRequest, resetWalletResponse);
    }
  }
}
