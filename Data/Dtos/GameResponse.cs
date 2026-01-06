using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameFindr.Data.Dtos
{
    public class GameResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("year")]
        public double? Year { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("rating")]
        public RatingResponse? Rating { get; set; }

        [JsonPropertyName("adult_only")]
        public bool? AdultOnly { get; set; }

        [JsonPropertyName("screenshots")]
        public List<string>? Screenshots { get; set; }

        [JsonPropertyName("short_description")]
        public string? ShortDescription { get; set; }
    }
}
