namespace SteamTopGames.Models
{
    public class SteamGameDetails
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string name { get; set; }
        public string short_description { get; set; }
        public string header_image { get; set; }
    }
}
