using System.Collections.Generic;
using System.Linq;

namespace Reporting
{
    public class TradeVolumeCalculator
    {
        public static IDictionary<string, double> CalculateAggregateVolumes(IEnumerable<ITrade> trades)
        {
            IDictionary<int, string> mappings = PeriodTimeTradeMappings.GetMappings();
            IDictionary<int, double> periodVolumes = mappings.ToDictionary(kvp => kvp.Key, kvp => 0.0);

            foreach (var trade in trades)
            {
                foreach (var volumePerPeriod in trade.VolumePerPeriod)
                {
                    periodVolumes[volumePerPeriod.Key] += volumePerPeriod.Value;
                }
            }

            return periodVolumes.ToDictionary(kvp => mappings[kvp.Key], kvp => kvp.Value);
        }
    }
}
