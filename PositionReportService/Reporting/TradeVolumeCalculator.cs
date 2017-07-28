using Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reporting
{
    /// <summary>
    /// Exposes methods to calculate trade volumes.
    /// </summary>
    public class TradeVolumeCalculator : ITradeVolumeCalculator
    {
        private IServiceLogger logger;

        public TradeVolumeCalculator(IServiceLogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Calculates aggregate trade volumes.
        /// </summary>
        /// <param name="trades">A collection of trades.</param>
        /// <returns>Period-volume mappings based on the type of trade.</returns>
        public IDictionary<string, double> CalculateAggregateVolumes(IEnumerable<ITrade> trades)
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
                this.logger.LogEvent(ServiceEvent.VolumeCalculationFailed, ex.Message);
            }

            return result;
        }
    }
}
