using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameFindr.Data.Dtos
{
    public class GameListResponse
    {
        [JsonPropertyName("total_results")]
        public int? Total { get; set; }

        [JsonPropertyName("results")]
        public List<GameResponse>? Games { get; set; }
    }
}
