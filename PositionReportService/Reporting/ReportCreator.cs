using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reporting
{
    public class ReportCreator
    {
        public static void CreateTradeVolumeReport(IEnumerable<ITrade> trades, string savePath)
        {
            StringBuilder csv = new StringBuilder();
            IDictionary<string, double> aggregateVolumes = TradeVolumeCalculator.CalculateAggregateVolumes(trades);

            csv.AppendLine("Local Time,Volume");

            foreach (var volumePerPeriod in aggregateVolumes)
            {
                csv.AppendLine(string.Format("{0},{1}", volumePerPeriod.Key, Math.Round(volumePerPeriod.Value, 0)));
            }

            if (Directory.Exists(savePath))
            {
                string fullpath = Path.Combine(savePath, GetReportName());

                File.WriteAllText(fullpath, csv.ToString());
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(savePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static string GetReportName()
        {
            string prefix = string.Empty;

            switch (Utils.GetTradeType())
            {
                case Trade.PowerTrade:
                    prefix = "PowerPosition_";

                    break;
                default:
                    break;
            }
            
            DateTime gmtTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            string datetTimeComponent = gmtTime.ToString("yyyyMMdd_HHmm");
            string extension = ".csv";
            string reportName = prefix + datetTimeComponent + extension;

            return reportName;
        }
    }
}
