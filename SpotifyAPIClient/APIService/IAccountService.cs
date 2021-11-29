using SpotifyAPIClient.Models;

namespace SpotifyAPIClient.APIService
{
    public interface IAccountService
    {
        /// <summary>
        /// Gets the access token
        /// </summary>
        /// <returns>AccessToken</returns>
        Task<AccessToken> GetToken(); 
    }
}
