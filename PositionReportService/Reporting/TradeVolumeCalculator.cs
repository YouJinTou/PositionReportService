using Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reporting
{
    internal class TradeVolumeCalculator
    {
        public static IDictionary<string, double> CalculateAggregateVolumes(IEnumerable<ITrade> trades)
        {
            IDictionary<int, string> mappings = PeriodTimeTradeMappings.GetMappings();
            IDictionary<int, double> periodVolumes = mappings.ToDictionary(kvp => kvp.Key, kvp => 0.0);
            IDictionary<string, double> result = new Dictionary<string, double>();

            try
            {
                foreach (var trade in trades)
                {
                    foreach (var volumePerPeriod in trade.VolumePerPeriod)
                    {
                        periodVolumes[volumePerPeriod.Key] += volumePerPeriod.Value;
                    }
                }

                result = periodVolumes.ToDictionary(kvp => mappings[kvp.Key], kvp => kvp.Value);
            }
            catch (Exception ex)
            {
                ServiceLogger.LogEvent(ServiceEvent.VolumeCalculationFailed, new ConsoleLogStrategy(), ex.Message);
            }

            return result;
        }
    }
}
