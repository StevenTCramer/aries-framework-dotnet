namespace BlazorHosted.Features.Wallets
{
  using BlazorHosted.Features.Bases;
  using MediatR;

  public class ResetWalletRequest : BaseApiRequest, IRequest<ResetWalletResponse>
  {
    public const string RouteTemplate = BaseRequest.BaseUri + "agent/reset";

    internal override string GetRoute() => $"{RouteTemplate}?{nameof(CorrelationId)}={CorrelationId}";
  }
}
