using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameFindr.Data.Dtos
{
    public class RatingResponse
    { 

        [JsonPropertyName("mean")]
        public double? Mean { get; set; }

        [JsonPropertyName("count")]
        public double? Count { get; set; }
    }
}
