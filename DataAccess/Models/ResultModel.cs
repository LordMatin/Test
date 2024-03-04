using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ResultModel
    {
        public Int64 flight_id { get; set; }
        public Int64 origin_city_id { get; set; } 
        public Int64 destination_city_id { get; set; }
        public DateTime departure_time { get; set; }
        public DateTime arrival_time { get; set; }
        public Int64 airline_id { get; set; }
        public string status { get; set; }


    }
}
