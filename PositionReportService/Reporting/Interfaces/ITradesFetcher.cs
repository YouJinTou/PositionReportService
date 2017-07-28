using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reporting
{
    public interface ITradesFetcher
    {
        Task<IEnumerable<ITrade>> GetTradesAsync(DateTime date);
    }
}
