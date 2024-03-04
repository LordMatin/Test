using Models.flights;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.routes
{
    public class Route
    {
        [Required]
        [Key]
        public Int64 route_id { get; set; }
        [Required]

        public Int64 origin_city_id { get; set; }
        [Required]

        public Int64 destination_city_id { get; set; }
        [Required]

        public DateTime departure_date { get; set; }

    }
}
