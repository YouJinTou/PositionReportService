namespace Logging
{
    /// <summary>
    /// Exposes methods for logging report events.
    /// </summary>
    public class ServiceLogger : IServiceLogger
    {
        private ILogStrategy strategy;

        /// <summary>
        /// Instantiates a logger.
        /// </summary>
        /// <param name="logStrategy">
        /// The logging strategy. Available:
        /// <para>Console</para>
        /// <para>Windows Event Log</para>
        /// </param>
        public ServiceLogger(LogStrategy logStrategy)
        {
            switch (logStrategy)
            {
                case LogStrategy.Console:
                    this.strategy = new ConsoleLogStrategy();

                    break;
                case LogStrategy.WindowsEventLog:
                    this.strategy = new WindowsEventLogStrategy();

                    break;
                default:
                    this.strategy = new WindowsEventLogStrategy();

                    break;
            }
        }

        /// <summary>
        /// Logs a given event.
        /// </summary>
        /// <param name="serviceEvent">The service event.</param>        
        /// <param name="message">The message to pass along.</param>
        public void LogEvent(ServiceEvent serviceEvent, string message = null)
        {
            this.strategy.LogEvent(serviceEvent, message);
        }

        /// <summary>
        /// Logs a given event. A logging strategy can be chosen.
        /// </summary>
        /// <param name="serviceEvent">The service event.</param>
        /// <param name="logStrategy">The strategy to be used</param>
        /// <param name="message">The message to pass along.</param>
        public static void LogEvent(ServiceEvent serviceEvent, LogStrategy logStrategy, string message = null)
        {
            ILogStrategy loggingStrategy;

            switch (logStrategy)
            {
                case LogStrategy.Console:
                    loggingStrategy = new ConsoleLogStrategy();

                    break;
                case LogStrategy.WindowsEventLog:
                    loggingStrategy = new WindowsEventLogStrategy();

                    break;
                default:
                    loggingStrategy = new WindowsEventLogStrategy();

                    break;
            }

            loggingStrategy.LogEvent(serviceEvent, message);
        }
    }
}
