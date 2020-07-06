namespace Hyperledger.Aries.AspNetCore.Features.Connections
{
  using System;
  using Hyperledger.Aries.AspNetCore.Features.Bases;
  using Hyperledger.Aries.Features.DidExchange;

  public class RecieveInvitationResponse : BaseResponse
  {
    public ConnectionInvitationMessage ConnectionInvitationMessage { get; set; } = null!;

    public RecieveInvitationResponse() { }

    public RecieveInvitationResponse(Guid aCorrelationId, ConnectionInvitationMessage aConnectionInvitationMessage)
      : base(aCorrelationId) 
    {
      ConnectionInvitationMessage = aConnectionInvitationMessage;
    }
  }
}
