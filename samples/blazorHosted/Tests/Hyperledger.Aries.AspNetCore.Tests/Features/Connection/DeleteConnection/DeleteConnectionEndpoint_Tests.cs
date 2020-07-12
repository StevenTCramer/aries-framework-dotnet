namespace DeleteConnectionEndpoint
{
  using FluentAssertions;
  using Hyperledger.Aries.AspNetCore.Features.Connections;
  using Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure;
  using System;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly DeleteConnectionRequest DeleteConnectionRequest;
    private readonly FaberApplication FaberApplication;

    public Returns(FaberApplication aFaberApplication)
    {
      DeleteConnectionRequest = new DeleteConnectionRequest("ConnectionId");
      FaberApplication = aFaberApplication;
    }

    public async Task DeleteConnectionResponse_using_Json_Net()
    {
      DeleteConnectionResponse deleteConnectionResponse =
        await FaberApplication.WebApiTestService.DeleteJsonAsync<DeleteConnectionResponse>(DeleteConnectionRequest.GetRoute());

      TestApplication.ValidateDeleteConnectionResponse(DeleteConnectionRequest, deleteConnectionResponse);
    }

    public async Task GetConnectionsResponse_using_System_Text_Json()
    {
      HttpResponseMessage httpResponseMessage = await FaberApplication.HttpClient.DeleteAsync(DeleteConnectionRequest.GetRoute());

      httpResponseMessage.EnsureSuccessStatusCode();

      DeleteConnectionResponse deleteConnectionResponse =
        await httpResponseMessage.Content.ReadFromJsonAsync<DeleteConnectionResponse>();

      deleteConnectionResponse.Should().NotBeNull();
    }

    public void ValidationError()
    {
      // Arrange Set invalid value
      DeleteConnectionRequest.ConnectionId = null;

      // Act & Assert
      DeleteConnectionRequest.Invoking(aGetConnectionRequest => aGetConnectionRequest.GetRoute())
        .Should().Throw<ArgumentNullException>();
    }
  }
}
