namespace Logging
{
    public abstract class LogStrategyBase
    {
        internal abstract void OnApiCallFailed();

        internal abstract void OnGenerationIntervalChanged();

        internal abstract void OnInvalidTradeTypeReceived();

        internal abstract void OnMaxApiCallsExceeded();

        internal abstract void OnReportCreatedSuccessfully();

        internal abstract void OnServiceInitialized();

        internal abstract void OnServiceStopped();

        internal abstract void OnSleeping();

        internal abstract void OnVolumeCalculationFailed();
    }
}
