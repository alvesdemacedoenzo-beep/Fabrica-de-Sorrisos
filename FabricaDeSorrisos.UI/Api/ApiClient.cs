using System.Net.Http;
using System.Net.Http.Headers;
using FabricaDeSorrisos.UI.Api;
using FabricaDeSorrisos.UI.Models;

public static class ApiClient
{
    private static HttpClient _client;

    public static HttpClient GetClient()
    {
        if (_client == null)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiSettings.BaseUrl)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        if (!string.IsNullOrWhiteSpace(UserSession.Token))
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", UserSession.Token);
        }
        else
        {
            _client.DefaultRequestHeaders.Authorization = null;
        }

        return _client;
    }
}
