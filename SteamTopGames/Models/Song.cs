using System.Collections.Generic;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace SteamTopGames.Models
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }

    
}
