﻿namespace SendRequestForProofHandler
{
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using BlazorHosted.Server.Integration.Tests.Infrastructure;
  using BlazorHosted.Features.PresentProofs;
  using BlazorHosted.Server;
  using FluentAssertions;

  public class Handle_Returns : BaseTest
  {
    private readonly SendRequestForProofRequest SendRequestForProofRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      SendRequestForProofRequest = new SendRequestForProofRequest { Days = 10 };
    }

    public async Task SendRequestForProofResponse()
    {
      SendRequestForProofResponse SendRequestForProofResponse = await Send(SendRequestForProofRequest);

      ValidateSendRequestForProofResponse(SendRequestForProofResponse);
    }

    private void ValidateSendRequestForProofResponse(SendRequestForProofResponse aSendRequestForProofResponse)
    {
      aSendRequestForProofResponse.CorrelationId.Should().Be(SendRequestForProofRequest.CorrelationId);
      // check Other properties here
    }

  }
}