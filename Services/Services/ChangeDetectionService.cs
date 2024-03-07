using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repositories;
using Models.flights;
using Models.routes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var lstflight = mydbcontext.Flights
                .Join(
                       mydbcontext.Routes,
                       f => f.route.route_id,
                       r => r.route_id,
                       (f, r) => new { TblFlight = f, tblRoute = r }
                )
                .Join(
                        mydbcontext.Subscriptions,
                        f => new { f.tblRoute.origin_city_id, f.tblRoute.destination_city_id },
                        s => new { s.origin_city_id, s.destination_city_id },
                        (f, s) => new { JoinTblFlightRoute = f, tblSubscription = s }
                  ).Where(x =>
                  x.JoinTblFlightRoute.TblFlight.route.departure_date >= startdate &&
                  x.JoinTblFlightRoute.TblFlight.route.departure_date <= enddate &&
                  x.tblSubscription.agency_id == agencyid
                  ).Select(x => new FlightDto
                  {
                      flight_id = x.JoinTblFlightRoute.TblFlight.flight_id,
                      departure_time = x.JoinTblFlightRoute.TblFlight.departure_time,
                      arrival_time = x.JoinTblFlightRoute.TblFlight.arrival_time,
                      airline_id = x.JoinTblFlightRoute.TblFlight.airline_id,
                      route = x.JoinTblFlightRoute.TblFlight.route,
                  }).ToList();
            try
            {
                result = Changedetectionstatus(lstflight);
                return result;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<ResultModel> Changedetectionstatus(List<FlightDto> _lstflight)//DateTime departure_time, Int64 airline_id)
        {
            try
            {
                var result = new List<ResultModel>();
                DateTime Lastsevendays = new DateTime();
                DateTime Nextsevendays = new DateTime();
                var NextMin = new DateTime();
                var LastMin = new DateTime();
                var lstDepartureTimeLastweek = new List<Flight>();
                var lstDepartureTimeNextweek = new List<Flight>();
                int batchSize = _lstflight.Count();
                int pageNumber = 1;
                bool hasMoreRecords = true;
                while (hasMoreRecords)
                {
                    var lst = mydbcontext.Flights
                    .AsNoTracking()
                    .OrderBy(e => e.flight_id)
                    .Skip((pageNumber - 1) * batchSize)
                    .Take(batchSize)
                    .ToList();
                    if (lst.Any())
                    {
                        _lstflight.ForEach(x =>

                        {
                            Lastsevendays = x.departure_time.AddDays(-7);
                            Nextsevendays = x.departure_time.AddDays(7);
                            NextMin = x.departure_time.AddMinutes(30);
                            LastMin = x.departure_time.AddMinutes(-30);
                            var airline_id = x.airline_id;

                            lstDepartureTimeLastweek = lst.Where(e => e.departure_time == Lastsevendays &&
                                                    e.airline_id == airline_id &&
                                                    (e.departure_time >= LastMin ||
                                                    e.departure_time <= NextMin)).ToList();


                            lstDepartureTimeNextweek = lst.Where(e => e.departure_time == Nextsevendays &&
                                                   e.airline_id == airline_id &&
                                                   (e.departure_time >= LastMin ||
                                                   e.departure_time <= NextMin)).ToList();

                            pageNumber++;
                        });
                    }
                    else
                    {
                        hasMoreRecords = false;
                    }
                }
                if (lstDepartureTimeNextweek.Count == 0)
                {
                    result = lstDepartureTimeLastweek.Select(model => new ResultModel
                    {
                        flight_id = model.flight_id,
                        origin_city_id = model.route.origin_city_id,
                        destination_city_id = model.route.destination_city_id,
                        arrival_time = model.arrival_time,
                        airline_id = model.airline_id,
                        departure_time = model.departure_time,
                        status = "New flights"
                    }).ToList();

                }

                else if (lstDepartureTimeNextweek.Count() == 0)
                {
                    result = lstDepartureTimeLastweek.Select(model => new ResultModel
                    {
                        flight_id = model.flight_id,
                        origin_city_id = model.route.origin_city_id,
                        destination_city_id = model.route.destination_city_id,
                        arrival_time = model.arrival_time,
                        airline_id = model.airline_id,
                        departure_time = model.departure_time,
                        status = "Discontinued flights"
                    }).ToList();

                }


                else
                {
                    result = lstDepartureTimeLastweek.Select(model => new ResultModel
                    {
                        flight_id = model.flight_id,
                        origin_city_id = model.route.origin_city_id,
                        destination_city_id = model.route.destination_city_id,
                        arrival_time = model.arrival_time,
                        airline_id = model.airline_id,
                        departure_time = model.departure_time,
                        status = "regular flights"
                    }).ToList();

                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
