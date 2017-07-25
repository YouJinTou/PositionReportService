﻿using Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services;

namespace Reporting
{
    /// <summary>
    /// Exposes methods to get trade data for a specific date.
    /// </summary>
    internal class TradesFetcher
    {
        private IPowerService service;
        private TradeType trade;

        /// <summary>
        /// Instantiate to be able to get the trades required.
        /// </summary>
        /// <param name="service">The type of API service.</param>
        /// <param name="trade">The type of trade.</param>
        public TradesFetcher(IPowerService service, TradeType trade)
        {
            this.service = service;
            this.trade = trade;

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

                    ServiceLogger.LogEvent(ServiceEvent.ApiCallFailed, new WindowsEventLogStrategy());

                    if (failCounter >= maxFailuresAllowed)
                    {
                        ServiceLogger.LogEvent(ServiceEvent.MaxApiCallsExceeded, new WindowsEventLogStrategy());

                        failCounter = 0;
                    }

                    await Task.Delay(1000);
                }
                catch (ArgumentException ae)
                {
                    ServiceLogger.LogEvent(ServiceEvent.InvalidTradeTypeReceived, new WindowsEventLogStrategy(), ae.Message);
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
