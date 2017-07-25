namespace Logging
{
    /// <summary>
    /// Exposes methods for logging report events.
    /// </summary>
    public static class ServiceLogger
    {
        /// <summary>
        /// Logs a given event. A logging strategy can be chosen.
        /// </summary>
        /// <param name="serviceEvent">The service event.</param>
        /// <param name="logStrategy">
        /// The strategy to be used. Currently:
        /// <para>ConsoleLogStrategy</para>
        /// <para>WindowsEventLogStrategy</para>
        /// </param>
        /// <param name="message">The message to pass along.</param>
        public static void LogEvent(ServiceEvent serviceEvent, ILogStrategy logStrategy, string message = null)
        {
            logStrategy.LogEvent(serviceEvent, message);
        }
    }
}
