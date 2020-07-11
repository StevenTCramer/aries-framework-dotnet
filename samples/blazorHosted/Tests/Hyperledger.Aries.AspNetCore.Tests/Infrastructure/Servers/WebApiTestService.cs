namespace Hyperledger.Aries.AspNetCore.Server.Integration.Tests.Infrastructure
{
  using FluentAssertions;
  using MediatR;
  using Newtonsoft.Json;
  using System.Net;
  using System.Net.Http;
  using System.Net.Mime;
  using System.Text;
  using System.Threading.Tasks;

  public class WebApiTestService
  {
    public readonly JsonSerializerSettings JsonSerializerSettings;
    protected readonly HttpClient HttpClient;

    public WebApiTestService(HttpClient aHttpClient)
    {
      JsonSerializerSettings = new JsonSerializerSettings();
      HttpClient = aHttpClient;
    }

    internal async Task ConfirmEndpointValidationError<TResponse>(string aRoute, IRequest<TResponse> aRequest, string aAttributeName)
    {
      HttpResponseMessage httpResponseMessage = await GetHttpResponseMessageFromPost(aRoute, aRequest);
      await ConfirmEndpointValidationError(httpResponseMessage, aAttributeName);
    }

    internal async Task ConfirmEndpointValidationError(HttpResponseMessage aHttpResponseMessage, string aAttributeName)
    {
      string json = await aHttpResponseMessage.Content.ReadAsStringAsync();

      aHttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      json.Should().Contain("errors");
      json.Should().Contain(aAttributeName);
    }

    internal async Task<TResponse> DeleteJsonAsync<TResponse>(string aUri)
    {
      HttpResponseMessage httpResponseMessage = await HttpClient.DeleteAsync(aUri);
      return await ReadFromJson<TResponse>(httpResponseMessage);
    }

    internal async Task<HttpResponseMessage> GetHttpResponseMessageFromPost<TResponse>(string aUri, IRequest<TResponse> aRequest)
    {
      string requestAsJson = JsonConvert.SerializeObject(aRequest, aRequest.GetType(), JsonSerializerSettings);

      var httpContent =
        new StringContent
        (
          requestAsJson,
          Encoding.UTF8,
          MediaTypeNames.Application.Json
        );

      HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(aUri, httpContent);
      return httpResponseMessage;
    }

    internal async Task<TResponse> GetJsonAsync<TResponse>(string aUri)
    {
      HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(aUri);

      return await ReadFromJson<TResponse>(httpResponseMessage);
    }

    internal async Task<TResponse> Post<TResponse>(string aUri, IRequest<TResponse> aRequest)
    {
      HttpResponseMessage httpResponseMessage = await GetHttpResponseMessageFromPost(aUri, aRequest);

      httpResponseMessage.EnsureSuccessStatusCode();

      string json = await httpResponseMessage.Content.ReadAsStringAsync();

      TResponse response = JsonConvert.DeserializeObject<TResponse>(json, JsonSerializerSettings);

      return response;
    }

    private async Task<TResponse> ReadFromJson<TResponse>(HttpResponseMessage aHttpResponseMessage)
    {
      aHttpResponseMessage.EnsureSuccessStatusCode();

      string json = await aHttpResponseMessage.Content.ReadAsStringAsync();

      TResponse response = JsonConvert.DeserializeObject<TResponse>(json, JsonSerializerSettings);

      return response;
    }
  }
}
