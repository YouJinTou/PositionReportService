using Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services;

namespace Reporting
{
    /// <summary>
    /// Exposes methods to get trade data for a specific date.
    /// </summary>
    public class TradesFetcher : ITradesFetcher
    {
        private IPowerService service;
        private TradeType trade;
        private IServiceLogger logger;

        /// <summary>
        /// Instantiate to be able to get the trades required.
        /// </summary>
        /// <param name="service">The type of API service.</param>
        /// <param name="trade">The type of trade.</param>
        public TradesFetcher(IPowerService service, TradeType trade, IServiceLogger logger)
        {
            this.service = service;
            this.trade = trade;
            this.logger = logger;

            Environment.SetEnvironmentVariable("Trade", trade.ToString());
        }

        /// <summary>
        /// Gets all trade volumes for a given date.
        /// </summary>
        /// <param name="date">The current time wherever the service is running from. It will be converted to GMT.</param>
        /// <returns>The trade volumes for that period, usually 24 per position.</returns>
        public async Task<IEnumerable<ITrade>> GetTradesAsync(DateTime date)
        {
            IEnumerable<ITrade> trades = null;
            int failCounter = 0;
            int maxFailuresAllowed = 5;

            while (trades == null)
            {
                try
                {
                    trades = await this.GetAdaptedTradesAsync(date);
                    failCounter = 0;
                }
                catch (PowerServiceException)
                {
                    failCounter++;

                    this.logger.LogEvent(ServiceEvent.ApiCallFailed);

                    if (failCounter >= maxFailuresAllowed)
                    {
                        this.logger.LogEvent(ServiceEvent.MaxApiCallsExceeded);

                        failCounter = 0;
                    }

                    await Task.Delay(1000);
                }
                catch (ArgumentException ae)
                {
                    this.logger.LogEvent(ServiceEvent.InvalidTradeTypeReceived, ae.Message);
                }
            }

            return trades;
        }

        private async Task<IEnumerable<ITrade>> GetAdaptedTradesAsync(DateTime date)
        {
            switch (this.trade)
            {
                case TradeType.PowerTrade:
                    IEnumerable<PowerTrade> powerTrades = await this.service.GetTradesAsync(date);
                    ICollection<PowerTradeAdapter> adaptedTrades = new List<PowerTradeAdapter>();

                    foreach (PowerTrade powerTrade in powerTrades)
                    {
                        adaptedTrades.Add(new PowerTradeAdapter(powerTrade));
                    }

                    return adaptedTrades;
                default:
                    throw new ArgumentException(string.Format("Invalid trade type. Received: {0}", this.trade));
            }
        }
    }
}
