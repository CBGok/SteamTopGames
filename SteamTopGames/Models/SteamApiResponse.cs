namespace SteamTopGames.Models
{
    public class SteamApiResponse
    {
        public Response response { get; set; }

    }
    public class Response
    {
        public List<Game> games { get; set; }
    }
    public class Game
    {
        public int appid { get; set; }
        public int player_count { get; set; }
        public int rank { get; set; }
    }
}
