using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameFindr.Data.Models
{
    public class Game
    {
        // All endpoints
        [JsonPropertyName("id")]
        public int? GameBrainId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("rating")]
        public Rating? Rating { get; set; }

        [JsonPropertyName("adult_only")]
        public bool? AdultOnly { get; set; }

        [JsonPropertyName("screenshots")]
        public List<string>? ScreenshotUrls { get; set; }

        [JsonPropertyName("short_description")]
        public string? ShortDescription { get; set; }


        // Game details

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("developer")]
        public string? Developer { get; set; }

        [JsonPropertyName("platforms")]
        public List<ResponseItem>? Platforms { get; set; }

        [JsonPropertyName("genres")]
        public List<ResponseItem>? Genres { get; set; }

        [JsonPropertyName("themes")]
        public List<ResponseItem>? Themes { get; set; }

        [JsonPropertyName("play_modes")]
        public List<ResponseItem>? PlayModes { get; set; }
    }
}
