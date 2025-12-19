using GameFindr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GameFindr.Services
{
    public class GameService
    {
        HttpClient httpClient;
        List<Game> games = new List<Game>();
        string apiKey = "ed284d0f5e3d4106acaa551ef8a61c1b";

        public GameService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

        }

        public async Task<List<Game>> GetGamesBySearchAsync(string search, int offset)
        {
            var query = search.Replace(" ", "+"); // GameBrain accepts + in query
            var baseUrl = "https://api.gamebrain.co/v1/games";
            var requestUrl = $"{baseUrl}?query={Uri.EscapeDataString(query)}&offset={offset}&limit=20"; // offset and limit for pagination

            var response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var gameResponse = JsonSerializer.Deserialize<GamesResponse>(json);
                if (gameResponse?.Games != null)
                {
                    games.AddRange(gameResponse.Games);
                }
            }
            return games;
        }
    }
}
