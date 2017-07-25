namespace Logging
{
    public interface IServiceLogger
    {
        void LogEvent(ServiceEvent serviceEvent, string message = null);
    }
}
