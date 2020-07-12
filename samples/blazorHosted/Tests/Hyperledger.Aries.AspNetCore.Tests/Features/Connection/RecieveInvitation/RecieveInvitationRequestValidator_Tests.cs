namespace ReceiveInvitationRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;

  public class Validate_Should : BaseTest
  {
    private ReceiveInvitationRequest ReceiveInvitationRequest { get; set; }
    private ReceiveInvitationRequestValidator ReceiveInvitationRequestValidator { get; set; }

    public Validate_Should()
    {
      ReceiveInvitationRequestValidator = new ReceiveInvitationRequestValidator();
      ReceiveInvitationRequest = TestApplication.CreateValidReceiveInvitationRequest();
    }

    public void Be_Valid()
    {
      ValidationResult validationResult = ReceiveInvitationRequestValidator.TestValidate(ReceiveInvitationRequest);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_InvitationDetails_is_empty()
    {
      ReceiveInvitationRequest.InvitationDetails = "";
      ReceiveInvitationRequestValidator
        .ShouldHaveValidationErrorFor
        (
          aReceiveInvitationRequest => aReceiveInvitationRequest.InvitationDetails,
          ReceiveInvitationRequest
        );
    }

    public void Have_error_when_InvitationDetails_is_null()
    {
      ReceiveInvitationRequest.InvitationDetails = null;
      ReceiveInvitationRequestValidator
        .ShouldHaveValidationErrorFor
        (
          aReceiveInvitationRequest => aReceiveInvitationRequest.InvitationDetails,
          ReceiveInvitationRequest
        );
    }
  }
}
