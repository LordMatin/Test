using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.flights
{
    public class Flight
    {
        public Int64 flight_id { get; set; }
        public Int64 route_id { get; set; }
        public DateTime departure_time { get; set; }
        public DateTime arrival_time { get; set; }
        public Int64 airline_id { get; set; }
    }
}
