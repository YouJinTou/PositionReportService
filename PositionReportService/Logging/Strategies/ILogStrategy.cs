namespace Logging
{
    public interface ILogStrategy
    {
        void LogEvent(ServiceEvent serviceEvent, string message);
    }
}
