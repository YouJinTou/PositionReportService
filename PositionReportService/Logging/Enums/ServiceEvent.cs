namespace Logging
{
    public enum ServiceEvent
    {
        ServiceInitialized = 0,
        ReportCreatedSuccessfully,
        ServiceStopped,
        StopExceptionThrown,
        GenerationIntervalChanged,
        VolumeCalculationFailed,
        InvalidTradeTypeReceived,
        MaxApiCallsExceeded,
        ApiCallFailed,
        Sleeping,
        ParseFailed,
        WaitingBeforeStop
    }
}
