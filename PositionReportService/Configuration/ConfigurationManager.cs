using Logging;
using Newtonsoft.Json;
using Reporting;
using Services;
using System;
using System.IO;

namespace Configuration
{
    public static class ConfigurationManager
    {
        private static ConfigurationSettings appSettings;

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
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new ConsoleLogStrategy(), ex.Message);

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
                        ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new ConsoleLogStrategy(), ex.Message);
                    }
                }

                return Directory.Exists(AppSettings.TradeReportsPath) ? AppSettings.TradeReportsPath : AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static TimeSpan GenerationIntervalInMinutes
        {
            get
            {
                int intervalInMinutes;
                bool parsed = int.TryParse(AppSettings.GenerationIntervalInMinutes, out intervalInMinutes);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new ConsoleLogStrategy(), "Could not parse interval.");
                }

                TimeSpan newInterval = (parsed && intervalInMinutes > 0) ? new TimeSpan(0, intervalInMinutes, 0) : new TimeSpan(0, 60, 0);

                return newInterval;
            }
        }

        public static ServiceType ServiceType
        {
            get
            {
                ServiceType serviceType;
                bool parsed = Enum.TryParse(AppSettings.ServiceType, out serviceType);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new ConsoleLogStrategy(), "Could not parse service type.");
                }

                return parsed ? serviceType : ServiceType.PowerService;
            }
        }

        public static TradeType TradeType
        {
            get
            {
                TradeType tradeType;
                bool parsed = Enum.TryParse(AppSettings.ServiceType, out tradeType);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new ConsoleLogStrategy(), "Could not parse trade type.");
                }

                return parsed ? tradeType : TradeType.PowerTrade;
            }
        }

        public static IPowerService Service
        {
            get
            {
                ServiceType serviceType;
                bool parsed = Enum.TryParse(AppSettings.ServiceType, out serviceType);

                if (!parsed)
                {
                    ServiceLogger.LogEvent(ServiceEvent.ParseFailed, new ConsoleLogStrategy(), "Could not parse service type.");
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

        public static void RefreshAppSettings()
        {
            appSettings = null;
        }
    }
}
