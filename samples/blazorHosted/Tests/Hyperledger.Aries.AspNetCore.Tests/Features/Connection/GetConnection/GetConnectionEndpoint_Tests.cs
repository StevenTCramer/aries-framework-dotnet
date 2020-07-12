namespace GetConnectionEndpoint
{
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Newtonsoft.Json;
  using System;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly FaberApplication FaberApplication;

    private CreateInvitationResponse CreateInvitationResponse { get; set; }
    private GetConnectionRequest GetConnectionRequest { get; set; }

    public Returns(FaberApplication aFaberApplication)
    {
      FaberApplication = aFaberApplication;
      GetConnectionRequest = TestApplication.CreateValidGetConnectionRequest();
    }

    public async Task GetConnectionResponse_using_Json_Net()
    {
      GetConnectionRequest.ConnectionId = CreateInvitationResponse.ConnectionRecord.Id;

      GetConnectionResponse getConnectionResponse =
        await FaberApplication.WebApiTestService.GetJsonAsync<GetConnectionResponse>(GetConnectionRequest.GetRoute());

      FaberApplication.ValidateGetConnectionResponse(GetConnectionRequest, getConnectionResponse);
    }

    public async Task GetConnectionResponse_using_System_Text_Json()
    {
      GetConnectionRequest.ConnectionId = CreateInvitationResponse.ConnectionRecord.Id;

      GetConnectionResponse getConnectionResponse =
        await FaberApplication.HttpClient.GetFromJsonAsync<GetConnectionResponse>(GetConnectionRequest.GetRoute());

      TestApplication.ValidateGetConnectionResponse(GetConnectionRequest, getConnectionResponse);
    }

    public async Task Setup()
    {
      await FaberApplication.ResetAgent();
      CreateInvitationResponse = await FaberApplication.CreateAnInvitation();
    }

    public void ValidationError()
    {
      // Arrange Set invalid value
      GetConnectionRequest.ConnectionId = null;

      // Act & Assert
      GetConnectionRequest.Invoking(aGetConnectionRequest => aGetConnectionRequest.GetRoute())
        .Should().Throw<ArgumentNullException>();
    }
  }
}
