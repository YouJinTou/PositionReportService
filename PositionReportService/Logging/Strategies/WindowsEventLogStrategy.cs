using System.Diagnostics;

namespace Logging
{
    /// <summary>
    /// Allows writing report events to the Windows event log under "Trade Reporting Service Event Log".
    /// </summary>
    public class WindowsEventLogStrategy : LogStrategyBase, ILogStrategy
    {
        private const string EventSource = "Trade Reporting Service Event Source";
        private const string EventLogName = "Trade Reporting Service Event Log";

        private EventLog eventLog;

        /// <summary>
        /// Logs a given event to the console.
        /// </summary>
        /// <param name="serviceEvent">The service event.</param>
        /// <param name="message">The message to pass along.</param>
        public void LogEvent(ServiceEvent serviceEvent, string message)
        {
            this.eventLog = new EventLog();

            if (!EventLog.SourceExists(EventSource))
            {
                EventLog.CreateEventSource(EventSource, EventLogName);
            }

            this.eventLog.Source = EventSource;
            this.eventLog.Log = EventLogName;

            base.LogServiceEvent(serviceEvent, message);
        }

        internal override void OnApiCallFailed()
        {
            this.eventLog.WriteEntry(
                string.Format("{0} -- Call to service API failed. {2}",
                Utils.GetCurrentGmtDateFormatted(),
                base.serviceEvent.ToString(),
                base.message));
        }

        internal override void OnGenerationIntervalChanged()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnInvalidTradeTypeReceived()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnMaxApiCallsExceeded()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnParseFailed()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnReportCreatedSuccessfully()
        {
            this.eventLog.WriteEntry(
                string.Format("{0} -- Report created successfully. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnServiceInitialized()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- Service started. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnServiceStopped()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- Service stopped. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnSleeping()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- Sleeping... {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnVolumeCalculationFailed()
        {
            this.eventLog.WriteEntry(
                string.Format("{0} -- Trade volume calculation failed. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnWaitingBeforeStop()
        {
            this.eventLog.WriteEntry(string.Format("{0} -- Waiting before stop... {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }
    }
}
