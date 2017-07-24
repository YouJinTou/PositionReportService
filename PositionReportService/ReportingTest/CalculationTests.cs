using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;
using System;
using System.Collections.Generic;

namespace ReportingTest
{
    [TestClass]
    public class CalculationTests
    {
        private List<ITrade> DummyTrades
        {
            get
            {
                return new List<ITrade>
                {
                    new DummyTrade(),
                    new DummyTrade()
                };
            }
        }

        [TestMethod]
        public void TradeVolumeCalculator_Should_Correctly_Calculate_Aggregate_PowerTrade_Volumes()
        {
            Environment.SetEnvironmentVariable("Trade", "PowerTrade");

            var aggregateVolumes = TradeVolumeCalculator.CalculateAggregateVolumes(this.DummyTrades);
            var mappings = PeriodTimeTradeMappings.GetMappings();

            for (int period = 1; period <= 24; period++)
            {
                var expectedVolume = (this.DummyTrades[0].VolumePerPeriod[period] + this.DummyTrades[1].VolumePerPeriod[period]);

                Assert.AreEqual(expectedVolume, aggregateVolumes[mappings[period]]);
            }
        }
    }
}
