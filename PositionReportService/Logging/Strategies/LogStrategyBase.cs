namespace Logging
{
    /// <summary>
    /// An abstract class for logging strategies. Instantiate with 
    /// <para>ConsoleLogStrategy</para>
    /// <para>WindowsEventLogStrategy</para>
    /// </summary>
    internal abstract class LogStrategyBase
    {
        protected ServiceEvent serviceEvent;
        protected string message;

        protected void LogServiceEvent(ServiceEvent serviceEvent, string message)
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
                case ServiceEvent.ParseFailed:
                    this.OnParseFailed();

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
                case ServiceEvent.WaitingBeforeStop:
                    this.OnWaitingBeforeStop();

                    break;
                default:
                    break;
            }
        }

        internal abstract void OnApiCallFailed();

        internal abstract void OnGenerationIntervalChanged();

        internal abstract void OnInvalidTradeTypeReceived();

        internal abstract void OnMaxApiCallsExceeded();

        internal abstract void OnParseFailed();

        internal abstract void OnReportCreatedSuccessfully();

        internal abstract void OnServiceInitialized();

        internal abstract void OnServiceStopped();

        internal abstract void OnSleeping();

        internal abstract void OnVolumeCalculationFailed();

        internal abstract void OnWaitingBeforeStop();
    }
}
