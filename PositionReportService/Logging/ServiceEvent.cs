namespace Logging
{
    public enum ServiceEvent
    {
        ServiceInitialized = 0,
        ReportCreatedSuccessfully,
        ServiceStopped,
        StopFailed,
        GenerationIntervalChanged,
        VolumeCalculationFailed,
        InvalidTradeTypeReceived,
        MaxApiCallsExceeded,
        ApiCallFailed,
        Sleeping,
        ParseFailed
    }
}
