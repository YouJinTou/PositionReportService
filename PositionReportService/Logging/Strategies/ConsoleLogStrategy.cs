using System;

namespace Logging
{
    /// <summary>
    /// Allows writing report events to the console.
    /// </summary>
    internal class ConsoleLogStrategy : LogStrategyBase, ILogStrategy
    {
        /// <summary>
        /// Logs a given event to the console.
        /// </summary>
        /// <param name="serviceEvent">The service event.</param>
        /// <param name="message">The message to pass along.</param>
        public void LogEvent(ServiceEvent serviceEvent, string message)
        {
            base.LogServiceEvent(serviceEvent, message);
        }

        internal override void OnApiCallFailed()
        {
            Console.WriteLine(
                string.Format("{0} -- Call to service API failed. {1}", 
                Utils.GetCurrentGmtDateFormatted(), 
                base.message));
        }

        internal override void OnGenerationIntervalChanged()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnInvalidTradeTypeReceived()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnMaxApiCallsExceeded()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));

            // Send mail
        }

        internal override void OnParseFailed()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnReportCreatedSuccessfully()
        {
            Console.WriteLine(
                string.Format("{0} -- Report created successfully. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnServiceInitialized()
        {
            Console.WriteLine(string.Format("{0} -- Service started. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnServiceStopped()
        {
            Console.WriteLine(string.Format("{0} -- Service stopped. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnSleeping()
        {
            Console.WriteLine(string.Format("{0} -- Sleeping... {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnVolumeCalculationFailed()
        {
            Console.WriteLine(
                string.Format("{0} -- Trade volume calculation failed. {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }

        internal override void OnWaitingBeforeStop()
        {
            Console.WriteLine(string.Format("{0} -- Waiting before stop... {1}", Utils.GetCurrentGmtDateFormatted(), base.message));
        }
    }
}
