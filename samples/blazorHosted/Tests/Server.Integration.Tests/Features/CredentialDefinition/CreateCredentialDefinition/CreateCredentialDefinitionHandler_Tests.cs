﻿namespace CreateCredentialDefinitionHandler
{
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using BlazorHosted.Server.Integration.Tests.Infrastructure;
  using BlazorHosted.Features.CredentialDefinitions;
  using BlazorHosted.Server;
  using FluentAssertions;

  public class Handle_Returns : BaseTest
  {
    private readonly CreateCredentialDefinitionRequest CreateCredentialDefinitionRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      CreateCredentialDefinitionRequest = new CreateCredentialDefinitionRequest { Days = 10 };
    }

    public async Task CreateCredentialDefinitionResponse()
    {
      CreateCredentialDefinitionResponse CreateCredentialDefinitionResponse = await Send(CreateCredentialDefinitionRequest);

      ValidateCreateCredentialDefinitionResponse(CreateCredentialDefinitionResponse);
    }

    private void ValidateCreateCredentialDefinitionResponse(CreateCredentialDefinitionResponse aCreateCredentialDefinitionResponse)
    {
      aCreateCredentialDefinitionResponse.CorrelationId.Should().Be(CreateCredentialDefinitionRequest.CorrelationId);
      // check Other properties here
    }

  }
}