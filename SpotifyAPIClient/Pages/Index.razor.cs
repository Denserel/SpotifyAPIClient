
using Microsoft.AspNetCore.Components;
using SpotifyAPIClient.APIService;
using SpotifyAPIClient.Dtos;
using SpotifyAPIClient.Models;

namespace SpotifyAPIClient.Pages;

public partial class Index
{
    [Inject]
    IApiService apiService { get; set; }
    
    private IEnumerable<NewReleaseDto> releases;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var newReleases = await apiService.GetNewReleases();
            releases = NewReleaseToDto(newReleases);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private IEnumerable<NewReleaseDto> NewReleaseToDto(NewReleases newReleases)
    {
        var releases = newReleases.albums.items.Select(item => new NewReleaseDto
        {
            ImageUrl = item.images.FirstOrDefault().url,
            Name = item.name,
            Date = item.release_date,
            Artist = string.Join(",", item.artists.Select(artist => artist.name))
        });

        return releases;
    }
}

