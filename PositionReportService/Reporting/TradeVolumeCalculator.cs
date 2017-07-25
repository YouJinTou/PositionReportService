﻿using Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reporting
{
    /// <summary>
    /// Exposes methods to calculate trade volumes.
    /// </summary>
    internal class TradeVolumeCalculator
    {
        /// <summary>
        /// Calculates aggregate trade volumes.
        /// </summary>
        /// <param name="trades">A collection of trades.</param>
        /// <returns>Period-volume mappings based on the type of trade.</returns>
        public static IDictionary<string, double> CalculateAggregateVolumes(IEnumerable<ITrade> trades)
        {
            IDictionary<int, string> mappings = PeriodTimeMappings.GetMapping();
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
                ServiceLogger.LogEvent(ServiceEvent.VolumeCalculationFailed, new WindowsEventLogStrategy(), ex.Message);
            }

            return result;
        }
    }
}
