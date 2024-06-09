using Microsoft.AspNetCore.Mvc;
using SteamTopGames.Models;
using System.Text.Json;

namespace SteamTopGames.Controllers
{
    public class SteamController : Controller
    {
       
            private readonly HttpClient client = new HttpClient();

            public async Task<IActionResult> Index()
            {
                var games = await GetTopGames();
                return View(games);
            }

            private async Task<List<SteamGame>> GetTopGames()
            {
                var games = new List<SteamGame>();

                // SteamSpy API'den popüler oyunları çekme
                var steamSpyResponse = await client.GetStringAsync("https://steamspy.com/api.php?request=top100in2weeks");
                var steamSpyData = JsonSerializer.Deserialize<Dictionary<string, SteamSpyApiResponse>>(steamSpyResponse);

                // En popüler ilk 10 oyunun AppId'lerini alalım
                var topGameAppIds = steamSpyData.Values.Take(10).Select(game => game.appid).ToList();

                foreach (var appId in topGameAppIds)
                {
                    var playersResponse = await client.GetStringAsync($"http://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid={appId}");
                    var playersData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(playersResponse);
                    int currentPlayers = playersData["response"]["player_count"];

                    var detailsResponse = await client.GetStringAsync($"http://store.steampowered.com/api/appdetails?appids={appId}");
                    var detailsData = JsonSerializer.Deserialize<Dictionary<string, SteamGameDetails>>(detailsResponse);

                    if (detailsData[appId.ToString()].success)
                    {
                        var gameDetails = detailsData[appId.ToString()].data;

                        var game = new SteamGame
                        {
                            AppId = appId,
                            Name = gameDetails.name,
                            CurrentPlayers = currentPlayers,
                            Description = gameDetails.short_description,
                            HeaderImage = gameDetails.header_image
                        };

                        games.Add(game);
                    }
                }

                // En çok oynanan ilk 10 oyunu alın
                return games.OrderByDescending(g => g.CurrentPlayers).Take(10).ToList();
            }
        
    }
}
