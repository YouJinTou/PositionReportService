using System;

namespace Reporting
{
    internal static class Utils
    {
        public static Trade GetTradeType()
        {
            Trade trade = (Trade)Enum.Parse(typeof(Trade), Environment.GetEnvironmentVariable("Trade"));

            return trade;
        }
    }
}
