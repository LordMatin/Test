using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.subscriptions
{
    public class Subscription
    {
        [Required]
        [Key]
        public Int64 subscription_id { get; set; }
        [Required]

        public Int64 agency_id { get; set; }
        [Required]

        public Int64 origin_city_id { get; set; }
        [Required]

        public Int64 destination_city_id { get; set; }
    }
}
