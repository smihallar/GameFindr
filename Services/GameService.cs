using GameFindr.Data.Dtos;
using GameFindr.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameFindr.Services
{
    public class GameService
    {
        readonly HttpClient httpClient;
        readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };
        public GameService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Game>> GetGamesBySearchAsync(string search, int offset)
        {
            try
            {
                var query = search?.Replace(" ", "+");

                // Extra validation for empty query
                if (string.IsNullOrWhiteSpace(query))
                    throw new ArgumentException("Search query cannot be empty.", nameof(search));

                var requestUrl = $"?query={Uri.EscapeDataString(query)}&offset={offset}&limit=20";

                using var response = await httpClient.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var message = string.IsNullOrWhiteSpace(errorContent) ? (response.ReasonPhrase ?? "Request failed") : errorContent;
                    throw new HttpRequestException(message);
                }

                var content = await response.Content.ReadAsStringAsync();
                var listResponse = JsonSerializer.Deserialize<GameListResponse>(content, jsonOptions);
                if (listResponse?.Games == null || listResponse.Games.Count == 0)
                    return new List<Game>();

                return listResponse.Games.Select(MapFromGameResponse).ToList();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex);
            }
        }
        public async Task<Game?> GetGameDetailsByIdAsync(int id)
        {
            try
            {
                var requestUrl = $"{id}";

                using var response = await httpClient.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var message = string.IsNullOrWhiteSpace(errorContent) ? (response.ReasonPhrase ?? "Request failed") : errorContent;
                    throw new HttpRequestException(message);
                }

                var content = await response.Content.ReadAsStringAsync();
                var details = JsonSerializer.Deserialize<GameDetailsResponse>(content, jsonOptions);
                if (details is null)
                    throw new InvalidOperationException("Unexpected response format for game details.");

                return MapFromGameDetailsResponse(details);
            }
            catch (Exception ex)
            { 
                throw new HttpRequestException(ex.Message, ex);
            }
        }

        public async Task<List<Game>> GetSimilarGamesByIdAsync(int id)
        {
            try
            {
                var requestUrl = $"{id}/similar";

                using var response = await httpClient.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var message = string.IsNullOrWhiteSpace(errorContent) ? (response.ReasonPhrase ?? "Request failed") : errorContent;
                    throw new HttpRequestException(message);
                }

                var content = await response.Content.ReadAsStringAsync();
                var listResponse = JsonSerializer.Deserialize<GameListResponse>(content, jsonOptions);
                if (listResponse?.Games == null || listResponse.Games.Count == 0)
                    return new List<Game>();

                return listResponse.Games.Select(MapFromGameResponse).ToList();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex);
            }
        }

        // Helpers for mapping DTOs to Models

        Game MapFromGameResponse(GameResponse dto)
        {
            return new Game
            {
                GameBrainId = dto.Id,
                Name = dto.Name,
                Year = dto.Year.HasValue ? (int?)Convert.ToInt32(dto.Year.Value) : null,
                Genre = dto.Genre,
                Image = dto.Image ?? "default_image.png",
                Rating = MapRating(dto.Rating),
                AdultOnly = dto.AdultOnly,
                ScreenshotUrls = dto.Screenshots,
                ShortDescription = dto.ShortDescription
            };
        }

        Game MapFromGameDetailsResponse(GameDetailsResponse dto)
        {
            return new Game
            {
                GameBrainId = dto.Id,
                Name = dto.Name,
                Year = dto.Year.HasValue ? (int?)Convert.ToInt32(dto.Year.Value) : null,
                Image = dto.Image ?? "default_image.png",
                Rating = MapRating(dto.Rating),
                Description = dto.Description,
                ShortDescription = dto.ShortDescription,
                Developer = dto.Developer,
                Genre = dto.Genre,
                ScreenshotUrls = dto.Screenshots,
                Platforms = MapValueNameList(dto.Platforms),
                Genres = MapValueNameList(dto.Genres),
                Themes = MapValueNameList(dto.Themes),
                PlayModes = MapValueNameList(dto.PlayModes),
                AdultOnly = dto.AdultOnly
            };
        }

        Rating? MapRating(RatingResponse? dto)
        {
            if (dto is null) return null;
            return new Rating
            {
                Mean = dto.Mean,
                Count = dto.Count.HasValue ? (double?)dto.Count.Value : null
            };
        }

        List<ValueName>? MapValueNameList(List<ValueNameResponse>? dtos)
        {
            if (dtos is null) return null;
            return dtos.Select(d => new ValueName
            {
                Value = d.Value,
                Name = d.Name
            }).ToList();
        }
    }
}
