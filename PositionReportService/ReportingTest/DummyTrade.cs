using Reporting;
using System;
using System.Collections.Generic;

namespace ReportingTest
{
    internal class DummyTrade : ITrade
    {
        public DateTime Date
        {
            get
            {
                return DateTime.Now;
            }
        }

        public IDictionary<int, double> VolumePerPeriod
        {
            get
            {
                return new Dictionary<int, double>
                {
                    { 1, 10 },
                    { 2, 20 },
                    { 3, 30 },
                    { 4, 40 },
                    { 5, 50 },
                    { 6, 60 },
                    { 7, 70 },
                    { 8, 80 },
                    { 9, 90 },
                    { 10, 100 },
                    { 11, 90 },
                    { 12, 80 },
                    { 13, 70 },
                    { 14, 60 },
                    { 15, 50 },
                    { 16, 40 },
                    { 17, 30 },
                    { 18, 20 },
                    { 19, 10 },
                    { 20, 0 },
                    { 21, -10 },
                    { 22, -20 },
                    { 23, -30 },
                    { 24, -40 }
                };
            }
        }
    }
}
