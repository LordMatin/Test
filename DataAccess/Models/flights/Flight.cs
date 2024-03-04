using Models.routes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.flights
{
    public class Flight
    {
        [Required]
        [Key]
        public Int64 flight_id { get; set; }
        [Required]

        public DateTime departure_time { get; set; }
        [Required]

        public DateTime arrival_time { get; set; }
        [Required]

        public Int64 airline_id { get; set; }

        [Required]
        [ForeignKey("route")]
        public Int64 route_id { get; set; }
        public virtual Route route { get; set; }
    }
    public class FlightDto
    {
        [Required]
        [Key]
        public Int64 flight_id { get; set; }
        [Required]

        public DateTime departure_time { get; set; }
        [Required]

        public DateTime arrival_time { get; set; }
        [Required]

        public Int64 airline_id { get; set; }
        [Required]

        public virtual Route route { get; set; }
    }
}
