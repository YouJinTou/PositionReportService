using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reporting;
using System;
using System.Collections.Generic;

namespace Testing
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

            var mockLogger = new Mock<IServiceLogger>();
            var calculator = new TradeVolumeCalculator(mockLogger.Object);
            var aggregateVolumes = calculator.CalculateAggregateVolumes(this.DummyTrades);
            var mappings = PeriodTimeMappings.GetMapping();

            for (int period = 1; period <= 24; period++)
            {
                var expectedVolume = (this.DummyTrades[0].VolumePerPeriod[period] + this.DummyTrades[1].VolumePerPeriod[period]);

                Assert.AreEqual(expectedVolume, aggregateVolumes[mappings[period]]);
            }
        }
    }
}
