using System.Collections.Generic;

namespace Reporting
{
    public interface ITradeVolumeCalculator
    {
        IDictionary<string, double> CalculateAggregateVolumes(IEnumerable<ITrade> trades);
    }
}
