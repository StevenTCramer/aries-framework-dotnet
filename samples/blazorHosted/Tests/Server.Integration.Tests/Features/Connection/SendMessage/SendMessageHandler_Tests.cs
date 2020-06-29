﻿//namespace SendMessageHandler
//{
//  using System.Threading.Tasks;
//  using System.Text.Json;
//  using Microsoft.AspNetCore.Mvc.Testing;
//  using BlazorHosted.Server.Integration.Tests.Infrastructure;
//  using BlazorHosted.Server;
//  using FluentAssertions;
//  using BlazorHosted.Features.BasicMessaging;

//  public class Handle_Returns : BaseTest
//  {
//    private readonly SendMessageRequest SendMessageRequest;

//    public Handle_Returns
//    (
//      WebApplicationFactory<Startup> aWebApplicationFactory,
//      JsonSerializerOptions aJsonSerializerOptions
//    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
//    {
//      SendMessageRequest = new SendMessageRequest { Message = "Hello World" };
//    }

//    public async Task SendMessageResponse()
//    {
//      SendMessageResponse sendMessageResponse = await Send(SendMessageRequest);

//      ValidateSendMessageResponse(sendMessageResponse);
//    }

//    private void ValidateSendMessageResponse(SendMessageResponse aSendMessageResponse)
//    {
//      aSendMessageResponse.CorrelationId.Should().Be(SendMessageRequest.CorrelationId);
//      // check Other properties here
//    }

//  }
//}