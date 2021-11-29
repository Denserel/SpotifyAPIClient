using SpotifyAPIClient.Models;

namespace SpotifyAPIClient.APIService
{
    public interface IApiService
    {
        Task <NewReleases> GetNewReleases();
    }
}
