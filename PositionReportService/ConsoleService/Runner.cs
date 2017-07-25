using Configuration;
using Logging;
using Reporting;
using System;
using System.Threading.Tasks;

namespace ConsoleService
{
    internal class Runner
    {
        private static TimeSpan initialInterval;
        private static IServiceLogger logger;

        static void Main(string[] args)
        {
            initialInterval = ConfigurationManager.GenerationIntervalInMinutes;
            logger = new ServiceLogger(LogStrategy.Console);

            logger.LogEvent(ServiceEvent.ServiceInitialized, string.Format("Interval: {0}", initialInterval));
            
            Task.Run(() => CreateReport()).GetAwaiter().GetResult();

            logger.LogEvent(ServiceEvent.ServiceStopped);
        }

        private static async Task CreateReport()
        {
            while (true)
            {
                await ReportCreator.CreateTradeVolumeReportAsync(
                    DateTime.Now, 
                    ConfigurationManager.TradeReportsPath, 
                    ConfigurationManager.Service, 
                    ConfigurationManager.TradeType,
                    logger);

                logger.LogEvent(ServiceEvent.ReportCreatedSuccessfully);

                ConfigurationManager.RefreshAppSettings();

                TimeSpan newInterval = ConfigurationManager.GenerationIntervalInMinutes;

                if (IntervalChanged(newInterval))
                {
                    initialInterval = newInterval;

                    logger.LogEvent(ServiceEvent.GenerationIntervalChanged, string.Format("Interval changed to: {0}", newInterval));
                }

                logger.LogEvent(ServiceEvent.Sleeping);

                await Task.Delay(newInterval);
            }            
        }

        private static bool IntervalChanged(TimeSpan newInterval)
        {
            return (initialInterval != newInterval);
        }
    }
}
