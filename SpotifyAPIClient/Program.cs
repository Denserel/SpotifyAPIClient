using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpotifyAPIClient;
using SpotifyAPIClient.APIService;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using SpotifyAPIClient.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var spotifyApiSettings = builder.Configuration.GetSection(nameof(SpotifyApi)).Get<SpotifyApi>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IAccountService, AccountService>(
    client => client.BaseAddress = new Uri(spotifyApiSettings.AccountBaseUrl));

builder.Services.AddHttpClient<IApiService, APIService>(
    client => client.BaseAddress = new Uri(spotifyApiSettings.ApiBaseUrl));

await builder.Build().RunAsync();
