using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameFindr.Data.Dtos
{
    public class GameDetailsResponse
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("year")]
        public double? Year { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("rating")]
        public RatingResponse? Rating { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("short_description")]
        public string? ShortDescription { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("developer")]
        public string? Developer { get; set; }

        [JsonPropertyName("platforms")]
        public List<ValueNameResponse>? Platforms { get; set; }

        [JsonPropertyName("genres")]
        public List<ValueNameResponse>? Genres { get; set; }

        [JsonPropertyName("genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("themes")]
        public List<ValueNameResponse>? Themes { get; set; }

        [JsonPropertyName("adult_only")]
        public bool? AdultOnly { get; set; }

        [JsonPropertyName("play_modes")]
        public List<ValueNameResponse>? PlayModes { get; set; }

        [JsonPropertyName("screenshots")]
        public List<string>? Screenshots { get; set; }

      
    }
}
