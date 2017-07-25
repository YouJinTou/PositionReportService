using System;

namespace Reporting
{
    /// <summary>
    /// Exposes methods for miscellaneous methods used throughout the assembly.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Gets the current trade type based on the environment variable "Trade".
        /// </summary>
        /// <returns>The trade type.</returns>
        public static TradeType GetTradeType()
        {
            TradeType trade = (TradeType)Enum.Parse(typeof(TradeType), Environment.GetEnvironmentVariable("Trade"));

            return trade;
        }
    }
}
