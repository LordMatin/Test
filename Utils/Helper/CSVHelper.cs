using CsvHelper;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helper
{
    public static class CSVHelper
    {

        public static void WriteCsv(List<ResultModel> result)
        {
            using (var writer = new StreamWriter("Resultdata.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(result);
            }
        }
    }

}
