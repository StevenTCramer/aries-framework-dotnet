namespace Hyperledger.Aries.AspNetCore.Features.Connections
{
  using FluentValidation;
  
  public class RecieveInvitationRequestValidator : AbstractValidator<RecieveInvitationRequest>
  {

    public RecieveInvitationRequestValidator()
    {
      RuleFor(aRecieveInvitationRequest => aRecieveInvitationRequest.InvitationDetails)
        .NotEmpty();
    }
  }
}