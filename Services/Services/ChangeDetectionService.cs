using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories;
using Models.flights;
using Models.routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ChangeDetectionService
    {
        MyDbContext mydbcontext;

        public ChangeDetectionService()
        {
            mydbcontext = new MyDbContext();
        }
        public List<ResultModel> checkSitauts(DateTime startdate, DateTime enddate, Int64 agencyid)
        {
            List<ResultModel> result = new List<ResultModel>();
            BaseRepository<Flight> repository = new BaseRepository<Flight>(mydbcontext);
            //var lstflight = repository.Get(x => x.route_id.departure_date >= startdate && x.route_id.departure_date <= enddate, null, "route_id");

            var lstflight = mydbcontext.Flights
                .Join(
                       mydbcontext.Routes,
                       f => f.route_id.route_id,
                       r => r.route_id,
                       (f, r) => new { TblFlight = f, tblRoute = r }
                )
                .Join(
                        mydbcontext.Subscriptions,
                        f => new { f.tblRoute.origin_city_id, f.tblRoute.destination_city_id },
                        s => new { s.origin_city_id, s.destination_city_id },
                        (f, s) => new { JoinTblFlightRoute = f, tblSubscription = s }
                  ).Where(x =>
                  x.JoinTblFlightRoute.TblFlight.route_id.departure_date >= startdate &&
                  x.JoinTblFlightRoute.TblFlight.route_id.departure_date <= enddate &&
                  x.tblSubscription.agency_id == agencyid
                  ).Select(x => new FlightDto
                  {
                      flight_id = x.JoinTblFlightRoute.TblFlight.flight_id,
                      departure_time = x.JoinTblFlightRoute.TblFlight.departure_time,
                      arrival_time = x.JoinTblFlightRoute.TblFlight.arrival_time,
                      airline_id = x.JoinTblFlightRoute.TblFlight.airline_id,
                      route_id = x.JoinTblFlightRoute.TblFlight.route_id,
                  }).ToList();

            foreach (var item in lstflight)
            {
                result.Add(new ResultModel
                {
                    flight_id = item.flight_id,
                    origin_city_id = item.route_id.origin_city_id,
                    destination_city_id = item.route_id.destination_city_id,
                    departure_time = item.departure_time,
                    arrival_time = item.arrival_time,
                    airline_id = item.airline_id,
                    status = Changedetectionstatus(item.departure_time,item.airline_id ,lstflight)

                });

            }
            return result;
        }

        public string Changedetectionstatus(DateTime departure_time,Int64 airline_id, List<FlightDto> _lst)
        {
            DateTime Lastsevendays = departure_time.AddDays(-7);
            DateTime Nextsevendays = departure_time.AddDays(7);
            string NextMin = departure_time.AddMinutes(30).ToLongTimeString();
            string LastMin = departure_time.AddMinutes(-30).ToLongTimeString();


            var CountDepartureTimeLastweek = _lst.Where(cdt => cdt.departure_time == Lastsevendays)
                                    .ToList()
                                    .Where(cdt => cdt.airline_id == airline_id && (cdt.departure_time.ToLongTimeString() == NextMin || cdt.departure_time.ToLongTimeString() == LastMin)).Count();

            var CountDepartureTimeNextweek = _lst.Where(cdt => cdt.departure_time == Nextsevendays)
                                    .ToList()
                                    .Where(cdt => cdt.airline_id == airline_id && (cdt.departure_time.ToLongTimeString() == NextMin || cdt.departure_time.ToLongTimeString() == LastMin)).Count();


            //var CountDepartureTimeLastweek = _lst.Where(cdt => cdt.departure_time == Lastsevendays &&
            //                        (cdt.departure_time.ToLongTimeString() == NextMin || cdt.departure_time.ToLongTimeString() == LastMin) &&
            //                        cdt.airline_id == airline_id
            //                        ).Count();

            //var CountDepartureTimeNextweek = _lst.Where(cdt => cdt.departure_time == Nextsevendays &&
            //                        (cdt.departure_time.ToLongTimeString() == NextMin || cdt.departure_time.ToLongTimeString() == LastMin) &&
            //                        cdt.airline_id == airline_id).Count();

            if (CountDepartureTimeLastweek == 0)
                return "New flights";
            else if (CountDepartureTimeNextweek == 0)
                return "Discontinued flights";
            else
                return "regular";
        }

      
    }
}
