using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.routes
{
    public class Route
    {
        public Int64 route_id { get; set; }
        public Int64 origin_city_id { get; set; }
        public Int64 destination_city_id { get; set; }
        public DateTime departure_date { get; set; }
    }
}
