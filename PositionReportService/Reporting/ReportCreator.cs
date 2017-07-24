using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    public class ReportCreator
    {
        public static async Task CreateTradeVolumeReportAsync(DateTime date, string savePath, IPowerService service, TradeType tradeType)
        {
            TradesFetcher fetcher = new TradesFetcher(service, tradeType);
            IEnumerable<ITrade> trades = await fetcher.GetTradesAsync(date);
            StringBuilder csv = new StringBuilder();
            IDictionary<string, double> aggregateVolumes = TradeVolumeCalculator.CalculateAggregateVolumes(trades);

            csv.AppendLine("Local Time,Volume");

            foreach (var volumePerPeriod in aggregateVolumes)
            {
                csv.AppendLine(string.Format("{0},{1}", volumePerPeriod.Key, Math.Round(volumePerPeriod.Value, 0)));
            }

            string fullpath = Path.Combine(savePath, GetReportName());

            File.WriteAllText(fullpath, csv.ToString());
        }

        private static string GetReportName()
        {
            string prefix = string.Empty;

            switch (Utils.GetTradeType())
            {
                case TradeType.PowerTrade:
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
