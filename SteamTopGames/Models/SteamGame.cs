namespace SteamTopGames.Models
{
    public class SteamGame
    {
        public string Name { get; set; }
        public int CurrentPlayers { get; set; }
        public string Description { get; set; }
        public string HeaderImage { get; set; }
        public int AppId { get; set; }
    }
}
