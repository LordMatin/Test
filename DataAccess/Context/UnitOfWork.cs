using DataAccess.Repositories;
using Models.flights;
using Models.routes;
using Models.subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class UnitOfWork : IDisposable
    {
        MyDbContext db;
        public UnitOfWork()
        {
            db = new MyDbContext();
        }

        private BaseRepository<Flight> _FlightRepository;
        private BaseRepository<Route> _RouteRepository;
        private BaseRepository<Subscription> _SubscriptionRepository;
        public BaseRepository<Flight> FlightRepository
        {
            get
            {
                if (_FlightRepository == null)
                {
                    _FlightRepository = new BaseRepository<Flight>(db);
                }

                return _FlightRepository;
            }
        }
        public BaseRepository<Route> RouteRepository
        {
            get
            {
                if (_RouteRepository == null)
                {
                    _RouteRepository = new BaseRepository<Route>(db);
                }

                return _RouteRepository;
            }
        }
        public BaseRepository<Subscription> SubscriptionRepository
        {
            get
            {
                if (_SubscriptionRepository == null)
                {
                    _SubscriptionRepository = new BaseRepository<Subscription>(db);
                }

                return _SubscriptionRepository;
            }
        }

        public void Dispose()
        {
           db.Dispose();
        }
    }
}
