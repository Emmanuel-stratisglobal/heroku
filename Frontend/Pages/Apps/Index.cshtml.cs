using System.Configuration;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GettingStarted.Frontend.Pages;

public class AppIndexModel : PageModel
{ 
    public List<JsonElement> Apps { get; set; } = new();

    public async Task OnGetAsync()
    {
        var apiKey = Environment.GetEnvironmentVariable("HEROKU_API_KEY");

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.heroku+json; version=3");

        var response = await client.GetAsync("https://api.heroku.com/addons");
        var json = await response.Content.ReadAsStringAsync();
        Apps = JsonSerializer.Deserialize<List<JsonElement>>(json) ?? new();
    }
}
