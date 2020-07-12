namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using FluentAssertions;
  using Hyperledger.Aries.AspNetCore.Features.Wallets;
  using System.Threading.Tasks;

  public partial class TestApplication
  {
    internal static void ValidateResetWalletResponse
    (
      ResetWalletRequest aResetWalletRequest,
      ResetWalletResponse aResetWalletResponse
    ) => aResetWalletResponse.CorrelationId.Should().Be(aResetWalletRequest.CorrelationId);

    internal Task ResetAgent() => Send(new ResetWalletRequest());
  }
}
