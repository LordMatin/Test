using Models.flights;
using Models.routes;
using Models.subscriptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccess.Context
{
    public class MyDbContext:DbContext
    {
        public MyDbContext():base("MyContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<Flight> Flights { get; set; }   
        public DbSet<Route> Routes { get; set; }   
        public DbSet<Subscription> Subscriptions { get; set; }

  
    }
}
