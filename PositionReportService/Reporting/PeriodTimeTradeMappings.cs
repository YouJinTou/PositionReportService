﻿using System.Collections.Generic;

namespace Reporting
{
    public class PeriodTimeTradeMappings
    {
        private static readonly IDictionary<int, string> PowerTradeMappings = new Dictionary<int, string>
        {
            { 1, "23:00" },
            { 2, "00:00" },
            { 3, "01:00" },
            { 4, "02:00" },
            { 5, "03:00" },
            { 6, "04:00" },
            { 7, "05:00" },
            { 8, "06:00" },
            { 9, "07:00" },
            { 10, "08:00" },
            { 11, "09:00" },
            { 12, "10:00" },
            { 13, "11:00" },
            { 14, "12:00" },
            { 15, "13:00" },
            { 16, "14:00" },
            { 17, "15:00" },
            { 18, "16:00" },
            { 19, "17:00" },
            { 20, "18:00" },
            { 21, "19:00" },
            { 22, "20:00" },
            { 23, "21:00" },
            { 24, "22:00" }
        };

        public static IDictionary<int, string> GetMappings()
        {
            switch (Utils.GetTradeType())
            {
                case Trade.PowerTrade:
                    return PowerTradeMappings;
                default:
                    return PowerTradeMappings;
            }
        }
    }
}
