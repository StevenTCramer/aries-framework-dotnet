namespace RecieveInvitationRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Newtonsoft.Json;

  public class Validate_Should: BaseTest
  {
    private RecieveInvitationRequestValidator RecieveInvitationRequestValidator { get; set; }
    private RecieveInvitationRequest RecieveInvitationRequest { get; set; }

    public Validate_Should
    (
      AliceWebApplicationFactory aAliceWebApplicationFactory,
      JsonSerializerSettings aJsonSerializerSettings
    ) : base(aAliceWebApplicationFactory, aJsonSerializerSettings)
    {
      RecieveInvitationRequest = CreateValidRecieveInvitationRequest();
    }

    public void Be_Valid()
    {
      ValidationResult validationResult = RecieveInvitationRequestValidator.TestValidate(RecieveInvitationRequest);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_InvitationDetails_is_empty()
    {
      RecieveInvitationRequest.InvitationDetails = "";
      RecieveInvitationRequestValidator
        .ShouldHaveValidationErrorFor
        (
          aRecieveInvitationRequest => aRecieveInvitationRequest.InvitationDetails,
          RecieveInvitationRequest
        );
    }

    public void Have_error_when_InvitationDetails_is_null()
    {
      RecieveInvitationRequest.InvitationDetails = null;
      RecieveInvitationRequestValidator
        .ShouldHaveValidationErrorFor
        (
          aRecieveInvitationRequest => aRecieveInvitationRequest.InvitationDetails,
          RecieveInvitationRequest
        );
    }
  }
}
