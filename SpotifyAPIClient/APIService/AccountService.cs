using SpotifyAPIClient.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SpotifyAPIClient.APIService
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly SpotifyApi _spotifyApiSettings;

        public AccountService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _spotifyApiSettings = configuration.GetSection(nameof(SpotifyApi)).Get<SpotifyApi>();
        }
        
        public async Task<AccessToken> GetToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/token");

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_spotifyApiSettings.ClientId}:{_spotifyApiSettings.ClientSeacret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type","client_credentials"}
            });

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var accessToken = await JsonSerializer.DeserializeAsync<AccessToken>(responseStream);

            return accessToken;
        }
    }
}
