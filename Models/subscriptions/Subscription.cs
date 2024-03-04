using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.subscriptions
{
    public class Subscription
    {
        public Int64 subscription_id { get; set; }
        public Int64 agency_id { get; set; }
        public Int64 origin_city_id { get; set; }
        public Int64 destination_city_id { get; set; }
    }
}
