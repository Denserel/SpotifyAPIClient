using SpotifyAPIClient.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SpotifyAPIClient.APIService
{
    public class APIService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IAccountService _accountService;
        

        public APIService(HttpClient httpClient, IAccountService accountService)
        {
            _httpClient = httpClient;
            _accountService = accountService;
        }

        public async Task <NewReleases> GetNewReleases()
        {
            var accessToken = await _accountService.GetToken();

            var request = new HttpRequestMessage(HttpMethod.Get, "/v1/browse/new-releases");
            request.Headers.Authorization = new AuthenticationHeaderValue(accessToken.token_type, accessToken.access_token);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responsSteam = await response.Content.ReadAsStreamAsync();
            var responsNewReleases = await JsonSerializer.DeserializeAsync<NewReleases>(responsSteam);

            return responsNewReleases;
        }
    }
}
