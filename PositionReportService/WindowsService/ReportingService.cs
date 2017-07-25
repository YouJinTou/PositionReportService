using Configuration;
using Logging;
using Reporting;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService
{
    public partial class ReportingService : ServiceBase
    {
        private CancellationTokenSource cancellationTokenSource;
        private Task createReportTask;
        private TimeSpan initialInterval;

        public ReportingService()
        {
            this.initialInterval = ConfigurationManager.GenerationIntervalInMinutes;

            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(15000); // For attaching purposes

            ServiceLogger.LogEvent(ServiceEvent.ServiceInitialized, new WindowsEventLogStrategy());

            this.cancellationTokenSource = new CancellationTokenSource();
            this.createReportTask = Task.Run(() => this.CreateReport(this.cancellationTokenSource.Token));
        }

        protected override void OnStop()
        {
            this.cancellationTokenSource.Cancel();

            try
            {
                ServiceLogger.LogEvent(ServiceEvent.WaitingBeforeStop, new WindowsEventLogStrategy());

                this.RequestAdditionalTime(TimeSpan.FromSeconds(60).Milliseconds);

                this.createReportTask.Wait();
            }
            catch (AggregateException aex)
            {
                ServiceLogger.LogEvent(ServiceEvent.StopExceptionThrown, new WindowsEventLogStrategy(), aex.Message);
            }

            ServiceLogger.LogEvent(ServiceEvent.ServiceStopped, new WindowsEventLogStrategy());
        }

        private async Task CreateReport(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await ReportCreator.CreateTradeVolumeReportAsync(
                    DateTime.Now,
                    ConfigurationManager.TradeReportsPath,
                    ConfigurationManager.Service,
                    ConfigurationManager.TradeType);

                ServiceLogger.LogEvent(ServiceEvent.ReportCreatedSuccessfully, new WindowsEventLogStrategy());

                ConfigurationManager.RefreshAppSettings();

                TimeSpan newInterval = ConfigurationManager.GenerationIntervalInMinutes;

                if (this.IntervalChanged(newInterval))
                {
                    ServiceLogger.LogEvent(
                        ServiceEvent.GenerationIntervalChanged,
                        new WindowsEventLogStrategy(),
                        string.Format("Interval changed to: {0}", newInterval));
                }

                ServiceLogger.LogEvent(ServiceEvent.Sleeping, new WindowsEventLogStrategy());

                await Task.Delay(newInterval, token);
            }

            ServiceLogger.LogEvent(ServiceEvent.ServiceStopped, new WindowsEventLogStrategy());
        }

        private bool IntervalChanged(TimeSpan newInterval)
        {
            return (this.initialInterval != newInterval);
        }
    }
}
