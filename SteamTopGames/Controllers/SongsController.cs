using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using SteamTopGames.Services;
using SteamTopGames.Models;
using System.Text.Json.Serialization;

namespace SteamTopGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SpotifyService _spotifyService;

        public SongsController(IHttpClientFactory httpClientFactory, SpotifyService spotifyService)
        {
            _httpClientFactory = httpClientFactory;
            _spotifyService = spotifyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopSongs()
        {
            var accessToken = await _spotifyService.GetAccessToken();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetStringAsync("https://api.spotify.com/v1/playlists/2YRe7HRKNRvXdJBp9nXFza/tracks"); // Doğru endpoint'i kullanın
            var spotifyResponse = JsonSerializer.Deserialize<SpotifyTracksResponse>(response);

            var songs = spotifyResponse.Items.Select(item => new Song
            {
                Name = item.Track.Name,
                Artist = string.Join(", ", item.Track.Artists.Select(a => a.Name)),
                Url = item.Track.ExternalUrls.Spotify,
                ImageUrl = item.Track.Album.Images.FirstOrDefault()?.Url
            }).ToList();

            return Ok(songs);
        }
    }

    public class SpotifyTracksResponse
    {
        [JsonPropertyName("items")]
        public List<SpotifyTrackItem> Items { get; set; }
    }

    public class SpotifyTrackItem
    {
        [JsonPropertyName("track")]
        public SpotifyTrack Track { get; set; }
    }

    public class SpotifyTrack
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("artists")]
        public List<SpotifyArtist> Artists { get; set; }

        [JsonPropertyName("external_urls")]
        public SpotifyExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("album")]
        public SpotifyAlbum Album { get; set; }
    }

    public class SpotifyArtist
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class SpotifyExternalUrls
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; }
    }

    public class SpotifyAlbum
    {
        [JsonPropertyName("images")]
        public List<SpotifyImage> Images { get; set; }
    }

    public class SpotifyImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
