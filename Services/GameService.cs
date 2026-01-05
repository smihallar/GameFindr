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
        string baseUrl = "https://api.gamebrain.co/v1/games";

        public GameService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<List<Game>> GetGamesBySearchAsync(string search, int offset)
        {
            var query = search.Replace(" ", "+"); // GameBrain accepts + in query
            var requestUrl = $"{baseUrl}?query={Uri.EscapeDataString(query)}&offset={offset}&limit=20"; // offset and limit for pagination

            var response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var gamesResponse = JsonSerializer.Deserialize<GamesResponse>(json);
                if (gamesResponse?.Total > 0 && gamesResponse.Games != null)
                {
                    games.AddRange(gamesResponse.Games);
                }
            }
            return games;
        }

        public async Task<Game?> GetGameDetailsByIdAsync(int id)
        {
            var requestUrl = $"{baseUrl}/{id}";
            var response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var game = JsonSerializer.Deserialize<Game>(json);
                return game;
            }
            return null;
        }

        public async Task<List<Game>> GetSimilarGamesByIdAsync(int id)
        {
            var requestUrl = $"{baseUrl}/{id}/similar";
            var response = await httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var gamesResponse = JsonSerializer.Deserialize<GamesResponse>(json);
                if(gamesResponse?.Total > 0 && gamesResponse.Games != null)
                {
                    games.AddRange(gamesResponse.Games);
                }
            }
            return games;
        }
    }
}
