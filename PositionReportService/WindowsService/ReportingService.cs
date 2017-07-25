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
        private IServiceLogger logger;

        public ReportingService()
        {
            this.initialInterval = ConfigurationManager.GenerationIntervalInMinutes;
            this.logger = new ServiceLogger(LogStrategy.WindowsEventLog);

            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(15000); // For attaching purposes

            this.logger.LogEvent(ServiceEvent.ServiceInitialized);

            this.cancellationTokenSource = new CancellationTokenSource();
            this.createReportTask = Task.Run(() => this.CreateReport(this.cancellationTokenSource.Token));
        }

        protected override void OnStop()
        {
            this.cancellationTokenSource.Cancel();

            try
            {
                this.logger.LogEvent(ServiceEvent.WaitingBeforeStop);

                this.RequestAdditionalTime(TimeSpan.FromSeconds(60).Milliseconds);

                this.createReportTask.Wait();
            }
            catch (AggregateException aex)
            {
                this.logger.LogEvent(ServiceEvent.StopExceptionThrown, aex.Message);
            }

            this.logger.LogEvent(ServiceEvent.ServiceStopped);
        }

        private async Task CreateReport(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await ReportCreator.CreateTradeVolumeReportAsync(
                    DateTime.Now,
                    ConfigurationManager.TradeReportsPath,
                    ConfigurationManager.Service,
                    ConfigurationManager.TradeType,
                    this.logger);

                this.logger.LogEvent(ServiceEvent.ReportCreatedSuccessfully);

                ConfigurationManager.RefreshAppSettings();

                TimeSpan newInterval = ConfigurationManager.GenerationIntervalInMinutes;

                if (this.IntervalChanged(newInterval))
                {
                    this.logger.LogEvent(ServiceEvent.GenerationIntervalChanged, string.Format("Interval changed to: {0}", newInterval));
                }

                this.logger.LogEvent(ServiceEvent.Sleeping);

                await Task.Delay(newInterval, token);
            }

            this.logger.LogEvent(ServiceEvent.ServiceStopped);
        }

        private bool IntervalChanged(TimeSpan newInterval)
        {
            return (this.initialInterval != newInterval);
        }
    }
}
