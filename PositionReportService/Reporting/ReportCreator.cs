using Logging;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Reporting
{
    /// <summary>
    /// Exposes methods to create trade volume reports.
    /// </summary>
    public class ReportCreator
    {
        /// <summary>
        /// Creates a .csv report with two columns -- Local Time and Volume.
        /// </summary>
        /// <param name="date">The date of the report.</param>
        /// <param name="savePath">Where to save the report. Defaults to the executing assembly's base directory.</param>
        /// <param name="service">The type of API service to use.</param>
        /// <param name="tradeType">The type of trade.</param>
        /// <returns></returns>
        public static async Task CreateTradeVolumeReportAsync(DateTime date, string savePath, IPowerService service, TradeType tradeType, IServiceLogger logger)
        {
            TradesFetcher fetcher = new TradesFetcher(service, tradeType, logger);
            IEnumerable<ITrade> trades = await fetcher.GetTradesAsync(date);
            TradeVolumeCalculator calculator = new TradeVolumeCalculator(logger);
            IDictionary<string, double> aggregateVolumes = calculator.CalculateAggregateVolumes(trades);
            StringBuilder csv = new StringBuilder();

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
