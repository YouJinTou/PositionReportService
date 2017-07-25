using Logging;
using Newtonsoft.Json;
using Reporting;
using Services;
using System;
using System.IO;

namespace Configuration
{
    /// <summary>
    /// Allows to easily get the current service settings. Related to the config.json file in the executing assembly.
    /// The settings can be manually changed there, and the results will take effect at runtime.
    /// </summary>
    public static class ConfigurationManager
    {
        private static ConfigurationSettings appSettings;

        /// <summary>
        /// The directory where the reports are to be saved. Defaults to the executing assembly's base directory.
        /// </summary>
        public static string TradeReportsPath
        {
            get
            {
                if (!Directory.Exists(AppSettings.TradeReportsPath))
                {
                    try
                    {
                        Directory.CreateDirectory(AppSettings.TradeReportsPath);
                    }
                    catch (Exception ex)
                    {
                        ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new WindowsEventLogStrategy(), ex.Message);
                    }
                }

                return Directory.Exists(AppSettings.TradeReportsPath) ? AppSettings.TradeReportsPath : AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// The interval at which to generate reports, in minutes.
        /// </summary>
        public static TimeSpan GenerationIntervalInMinutes
        {
            get
            {
                int intervalInMinutes;
                bool parsed = int.TryParse(AppSettings.GenerationIntervalInMinutes, out intervalInMinutes);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new WindowsEventLogStrategy(), "Could not parse interval.");
                }

                TimeSpan newInterval = (parsed && intervalInMinutes > 0) ? new TimeSpan(0, intervalInMinutes, 0) : new TimeSpan(0, 60, 0);

                return newInterval;
            }
        }

        /// <summary>
        /// The type of API service. Currently:
        /// <para>- PowerService</para>
        /// </summary>
        public static ServiceType ServiceType
        {
            get
            {
                ServiceType serviceType;
                bool parsed = Enum.TryParse(AppSettings.ServiceType, out serviceType);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new WindowsEventLogStrategy(), "Could not parse service type.");
                }

                return parsed ? serviceType : ServiceType.PowerService;
            }
        }

        /// <summary>
        /// The type of trade. Currently:
        /// <para>- PowerTrade</para>
        /// </summary>
        public static TradeType TradeType
        {
            get
            {
                TradeType tradeType;
                bool parsed = Enum.TryParse(AppSettings.ServiceType, out tradeType);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new WindowsEventLogStrategy(), "Could not parse trade type.");
                }

                return parsed ? tradeType : TradeType.PowerTrade;
            }
        }

        /// <summary>
        /// Which API service to instantiate. Currently:
        /// <para>- PowerService</para>
        /// </summary>
        public static IPowerService Service
        {
            get
            {
                ServiceType serviceType;
                bool parsed = Enum.TryParse(AppSettings.ServiceType, out serviceType);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new WindowsEventLogStrategy(), "Could not parse service type.");
                }
                serviceType = parsed ? serviceType : ServiceType.PowerService;

                switch (serviceType)
                {
                    case ServiceType.PowerService:
                        return new PowerService();
                    default:
                        return new PowerService();
                }
            }
        }

        /// <summary>
        /// Use if you have made changes to config.json at runtime.
        /// </summary>
        public static void RefreshAppSettings()
        {
            appSettings = null;
        }

        private static ConfigurationSettings AppSettings
        {
            get
            {
                if (appSettings != null)
                {
                    return appSettings;
                }

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string settingsPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\", "config.json"));
                string jsonSettings = string.Empty;

                try
                {
                    jsonSettings = File.ReadAllText(settingsPath);
                    appSettings = JsonConvert.DeserializeObject<ConfigurationSettings>(jsonSettings);
                }
                catch (Exception ex)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new WindowsEventLogStrategy(), ex.Message);

                    appSettings = new ConfigurationSettings()
                    {
                        GenerationIntervalInMinutes = "60",
                        ServiceType = ServiceType.PowerService.ToString(),
                        TradeReportsPath = basePath,
                        TradeType = TradeType.PowerTrade.ToString()
                    };
                }

                return appSettings;
            }
        }
    }
}
