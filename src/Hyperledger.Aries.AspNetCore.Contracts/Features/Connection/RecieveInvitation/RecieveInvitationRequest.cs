namespace Hyperledger.Aries.AspNetCore.Features.Connections
{
  using Hyperledger.Aries.AspNetCore.Features.Bases;
  using MediatR;

  public class RecieveInvitationRequest : BaseApiRequest, IRequest<RecieveInvitationResponse>
  {
    public const string RouteTemplate = BaseRequest.BaseUri + "connections/recieve-invitation";

    public string InvitationDetails { get; set; } = null!;

    internal override string GetRoute() => $"{RouteTemplate}?{nameof(CorrelationId)}={CorrelationId}";

    public RecieveInvitationRequest(string aInvitationDetails)
    {
      InvitationDetails = aInvitationDetails;
    }
  }
}
