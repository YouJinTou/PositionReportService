using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporting;
using Services;

namespace ServiceRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                TradesFetcher fetcher = new TradesFetcher(new PowerService(), Trade.PowerTrade);

                ReportCreator.CreateTradeVolumeReport(await fetcher.GetTradesAsync(DateTime.Now), AppDomain.CurrentDomain.BaseDirectory);
            }).GetAwaiter().GetResult();
        }
    }
}
