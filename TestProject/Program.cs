using DataAccess.Context;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class Program
    {

        static void Main(string[] args)
        {
            try
            {
                ChangeDetectionService m = new ChangeDetectionService();
                //string start = "10-10-2016";
                //string End = "20-10-2016";
                DateTime startdateVal = Convert.ToDateTime(args[0]);
                DateTime EnddateVal = DateTime.Parse(args[1]);
                Int64 agencyid=Convert.ToInt64(args[2]);
                if(startdateVal>EnddateVal)
                {
                    Console.WriteLine("The Start Date cant be Greate than End Date");
                    Console.ReadKey();
                    return;
                }
                var r = m.checkSitauts(startdateVal, EnddateVal, agencyid);
                var result = string.Empty;
                foreach (var t in r.Take(100))
                {

                    result += string.Format("Flightid:{0},AirLineId:{1},OrginCityId:{2},DestintionCityId:{3},Stauts:{4}", t.flight_id
                   , t.airline_id, t.origin_city_id, t.destination_city_id, t.status);
                    result += "\n";
                }
                Console.WriteLine(result);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()+"\n"+ ex.InnerException?.Message);
                Console.ReadKey();

            }

        }
    }
}
