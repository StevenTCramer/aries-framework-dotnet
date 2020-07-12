namespace GetConnectionRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;

  public class Validate_Should : BaseTest
  {
    private GetConnectionRequest GetConnectionRequest { get; set; }
    private GetConnectionRequestValidator GetConnectionRequestValidator { get; set; }

    public Validate_Should()
    {
      GetConnectionRequestValidator = new GetConnectionRequestValidator();
      GetConnectionRequest = TestApplication.CreateValidGetConnectionRequest();
    }

    public void Be_Valid()
    {
      ValidationResult validationResult = GetConnectionRequestValidator.TestValidate(GetConnectionRequest);

      validationResult.IsValid.Should().BeTrue();
    }

    public void Have_error_when_ConnectionId_is_empty() => GetConnectionRequestValidator
          .ShouldHaveValidationErrorFor(aGetConnectionRequest => aGetConnectionRequest.ConnectionId, string.Empty);

    public void Have_error_when_ConnectionId_is_null()
    {
      GetConnectionRequest.ConnectionId = null;
      GetConnectionRequestValidator
        .ShouldHaveValidationErrorFor(aGetConnectionRequest => aGetConnectionRequest.ConnectionId, GetConnectionRequest);
    }
  }
}
