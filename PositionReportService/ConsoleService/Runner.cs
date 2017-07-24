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

        static void Main(string[] args)
        {
            initialInterval = ConfigurationManager.GenerationIntervalInMinutes;

            ServiceLogger.LogEvent(ServiceEvent.ServiceInitialized, new ConsoleLogStrategy(), string.Format("Interval: {0}", initialInterval));
            
            Task.Run(() => CreateReport()).GetAwaiter().GetResult();

            ServiceLogger.LogEvent(ServiceEvent.ServiceStopped, new ConsoleLogStrategy());
        }

        private static async Task CreateReport()
        {
            while (true)
            {
                await ReportCreator.CreateTradeVolumeReportAsync(
                    DateTime.Now, 
                    ConfigurationManager.TradeReportsPath, 
                    ConfigurationManager.Service, 
                    ConfigurationManager.TradeType);

                ServiceLogger.LogEvent(ServiceEvent.ReportCreatedSuccessfully, new ConsoleLogStrategy());

                ConfigurationManager.RefreshAppSettings();

                TimeSpan newInterval = ConfigurationManager.GenerationIntervalInMinutes;

                if (IntervalChanged(newInterval))
                {
                    initialInterval = newInterval;

                    ServiceLogger.LogEvent(
                        ServiceEvent.GenerationIntervalChanged, 
                        new ConsoleLogStrategy(), 
                        string.Format("Interval changed to: {0}", newInterval));
                }

                ServiceLogger.LogEvent(ServiceEvent.Sleeping, new ConsoleLogStrategy());

                await Task.Delay(newInterval);
            }            
        }

        private static bool IntervalChanged(TimeSpan newInterval)
        {
            return (initialInterval != newInterval);
        }
    }
}
