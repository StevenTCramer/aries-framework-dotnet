namespace GetConnectionsRequestValidator_
{
  using FluentAssertions;
  using FluentValidation.Results;
  using FluentValidation.TestHelper;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;

  public class Validate_Should : BaseTest
  {
    private GetConnectionsRequest GetConnectionsRequest { get; set; }
    private GetConnectionsRequestValidator GetConnectionsRequestValidator { get; set; }

    public Validate_Should()
    {
      GetConnectionsRequestValidator = new GetConnectionsRequestValidator();
      GetConnectionsRequest = TestApplication.CreateValidGetConnectionsRequest();
    }

    public void Be_Valid()
    {
      ValidationResult validationResult = GetConnectionsRequestValidator.TestValidate(GetConnectionsRequest);

      validationResult.IsValid.Should().BeTrue();
    }
  }
}
