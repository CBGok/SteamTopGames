using Microsoft.AspNetCore.Mvc;
using SteamTopGames.Models;
using System.Text.Json;

namespace SteamTopGames.Controllers
{
    public class SteamController : Controller
    {
        private readonly string apiKey = "3C7FC2BEB7015300718BF2E536C06B16";
        private readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            var games = await GetTopGames();
            return View(games);
        }

        private async Task<List<SteamGame>> GetTopGames()
        {
            var games = new List<SteamGame>();
            var mostPlayesGamesResponse = await client.GetStringAsync($"http://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?key={apiKey}");
            var mostPlayedGamesData = JsonSerializer.Deserialize<SteamApiResponse>(mostPlayesGamesResponse);

            foreach (var gameData in mostPlayedGamesData.response.games)
            {
                int appId = gameData.appid;
                int currentPlayers = gameData.player_count;

                var deailsResponse = await client.GetStringAsync($"http://store.steampowered.com/api/appdetails?appids={appId}");
                var detailsData = JsonSerializer.Deserialize<Dictionary<string, SteamGameDetails>>(deailsResponse);

                if (detailsData[appId.ToString()].success)
                {
                    var gameDetails = detailsData[appId.ToString()].data;

                    var game = new SteamGame()
                    {
                        AppId = appId,
                        Name = gameDetails.name,
                        CurrentPlayers = currentPlayers,
                        Description = gameDetails.short_description,
                        HeaderImage = gameDetails.header_image,
                    };

                    games.Add(game);
                }
            }

            return games.OrderByDescending(g => g.CurrentPlayers).Take(10).ToList();
            
        }
    }
}
