using System;

namespace Reporting
{
    internal static class Utils
    {
        public static TradeType GetTradeType()
        {
            TradeType trade = (TradeType)Enum.Parse(typeof(TradeType), Environment.GetEnvironmentVariable("Trade"));

            return trade;
        }
    }
}
