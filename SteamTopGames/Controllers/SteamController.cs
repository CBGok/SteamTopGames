using Microsoft.AspNetCore.Mvc;
using SteamTopGames.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SteamTopGames.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamGamesController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<IEnumerable<SteamGame>> Get()
        {
            var games = new List<SteamGame>();

            var steamSpyResponse = await client.GetStringAsync("https://steamspy.com/api.php?request=top100in2weeks");
            var steamSpyData = JsonSerializer.Deserialize<Dictionary<string, SteamSpyApiResponse>>(steamSpyResponse);

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

            return games.OrderByDescending(g => g.CurrentPlayers).Take(10).ToList();
        }
    }
}
