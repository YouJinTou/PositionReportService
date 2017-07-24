using Services;
using System;
using System.Collections.Generic;
using System.Linq;
    
namespace Reporting
{
    public class PowerTradeAdapter : ITrade
    {
        private PowerTrade powerTrade;

        public PowerTradeAdapter(PowerTrade powerTrade)
        {
            this.powerTrade = powerTrade;
        }

        public DateTime Date
        {
            get
            {
                return this.powerTrade.Date;
            }
        }

        public IDictionary<int, double> VolumePerPeriod
        {
            get
            {
                return this.powerTrade.Periods.ToDictionary(kvp => kvp.Period, kvp => kvp.Volume);
            }
        }
    }
}
