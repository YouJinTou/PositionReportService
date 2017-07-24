using System;

namespace Logging
{
    public class ConsoleLogStrategy : LogStrategyBase, ILogStrategy
    {
        private ServiceEvent serviceEvent;
        private string message;

        public void LogEvent(ServiceEvent serviceEvent, string message)
        {
            this.serviceEvent = serviceEvent;
            this.message = message;

            switch (serviceEvent)
            {
                case ServiceEvent.ApiCallFailed:
                    this.OnApiCallFailed();

                    break;
                case ServiceEvent.GenerationIntervalChanged:
                    this.OnGenerationIntervalChanged();

                    break;
                case ServiceEvent.InvalidTradeTypeReceived:
                    this.OnInvalidTradeTypeReceived();

                    break;
                case ServiceEvent.MaxApiCallsExceeded:
                    this.OnMaxApiCallsExceeded();

                    break;
                case ServiceEvent.ReportCreatedSuccessfully:
                    this.OnReportCreatedSuccessfully();

                    break;
                case ServiceEvent.ServiceInitialized:
                    this.OnServiceInitialized();

                    break;
                case ServiceEvent.ServiceStopped:
                    this.OnServiceStopped();

                    break;
                case ServiceEvent.Sleeping:
                    this.OnSleeping();

                    break;
                case ServiceEvent.VolumeCalculationFailed:
                    this.OnVolumeCalculationFailed();

                    break;
                default:
                    break;
            }
        }

        internal override void OnApiCallFailed()
        {
            Console.WriteLine(
                string.Format("{0} -- Call to service API failed. {2}", 
                Utils.GetCurrentGmtDateFormatted(), 
                this.serviceEvent.ToString(), 
                this.message));
        }

        internal override void OnGenerationIntervalChanged()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }

        internal override void OnInvalidTradeTypeReceived()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }

        internal override void OnMaxApiCallsExceeded()
        {
            Console.WriteLine(string.Format("{0} -- {1}", Utils.GetCurrentGmtDateFormatted(), this.message));

            // Send mail
        }

        internal override void OnReportCreatedSuccessfully()
        {
            Console.WriteLine(
                string.Format("{0} -- Report created successfully. {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }

        internal override void OnServiceInitialized()
        {
            Console.WriteLine(string.Format("{0} -- Service started. {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }

        internal override void OnServiceStopped()
        {
            Console.WriteLine(string.Format("{0} -- Service stopped. {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }

        internal override void OnSleeping()
        {
            Console.WriteLine(string.Format("{0} -- Sleeping... {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }

        internal override void OnVolumeCalculationFailed()
        {
            Console.WriteLine(
                string.Format("{0} -- Trade volume calculation failed. {1}", Utils.GetCurrentGmtDateFormatted(), this.message));
        }
    }
}
