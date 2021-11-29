
using Microsoft.AspNetCore.Components;
using SpotifyAPIClient.APIService;
using SpotifyAPIClient.Dtos;

namespace SpotifyAPIClient.Pages;

public partial class Index
{
    [Inject]
    IApiService apiService { get; set; }
    
    private IEnumerable<NewReleaseDto> release;
    
    protected override async Task OnInitializedAsync()
    {
        var newReleases = await apiService.GetNewReleases();
        
        release = newReleases.albums.items.Select(item => new NewReleaseDto
        {
            ImageUrl = item.images.FirstOrDefault().url,
            Name = item.name,
            Date = item.release_date,
            Artist = string.Join(",", item.artists.Select( artist => artist.name))
        });
    }
}

