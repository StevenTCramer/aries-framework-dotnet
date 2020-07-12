namespace CreateInvitationRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System.Linq;

  public class Validate_Should : BaseTest
  {
    private CreateInvitationRequest CreateInvitationRequest { get; set; }
    private CreateInvitationRequestValidator CreateInvitationRequestValidator { get; set; }

    public Validate_Should()
    {
      CreateInvitationRequestValidator = new CreateInvitationRequestValidator();
      CreateInvitationRequest = TestApplication.CreateValidCreateInvitationRequest();
    }

    public void Be_Valid()
    {
      ValidationResult validationResult = CreateInvitationRequestValidator.TestValidate(CreateInvitationRequest);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_AutoAccept_is_false()
    {
      CreateInvitationRequest.InviteConfiguration.AutoAcceptConnection = false;

      ValidationResult validationResult = CreateInvitationRequestValidator.TestValidate(CreateInvitationRequest);
      validationResult.Errors.Count.Should().BeGreaterOrEqualTo(1);
      validationResult.Errors.First().PropertyName.Should()
        .Be
        (
          $"{nameof(CreateInvitationRequest.InviteConfiguration)}." +
          $"{nameof(CreateInvitationRequest.InviteConfiguration.AutoAcceptConnection)}"
        );
    }

    public void Have_error_when_InvitationConfiguration_is_null()
    {
      CreateInvitationRequest.InviteConfiguration = null;

      ValidationResult validationResult = CreateInvitationRequestValidator.TestValidate(CreateInvitationRequest);
      validationResult.Errors.Count.Should().BeGreaterOrEqualTo(1);
    }
  }
}
