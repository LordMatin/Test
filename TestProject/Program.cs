using CsvHelper;
using DataAccess.Context;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper;

namespace TestProject
{
    internal class Program
    {

        static void Main(string[] args)
        {
            try
            {
                ChangeDetectionService m = new ChangeDetectionService();
                DateTime startdateVal = Convert.ToDateTime(args[0]);
                DateTime EnddateVal = DateTime.Parse(args[1]);
                Int64 agencyid=Convert.ToInt64(args[2]);
                if(startdateVal>EnddateVal)
                {
                    Console.WriteLine("The Start Date cant be Greate than End Date");
                    Console.ReadKey();
                    return;
                }
                var startTime = DateTime.Now;
                Console.WriteLine("Start Progress Time :"+ startTime +"\n"+"The Change Detection Algorithm is in progress ..... "+"\n");
                var checkSitauts = m.checkSitauts(startdateVal, EnddateVal, agencyid);
                CSVHelper.WriteCsv(checkSitauts);
                var EndTime = DateTime.Now;
                var DoneTime = EndTime - startTime;
                Console.WriteLine("End Progress Time :" + EndTime + "\n" + "The Reuslt Write on Resultdata.csv And Location on(\\bin\\Debug) Folder ..... " + "\n");
                Console.WriteLine("Done Time :" + DoneTime + "\n");
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
