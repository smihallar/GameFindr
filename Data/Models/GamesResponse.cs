using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFindr.Data.Models
{
    public class GamesResponse
    {
        public int Total { get; set; }
        public List<Game>? Games { get; set; }
    }
}
