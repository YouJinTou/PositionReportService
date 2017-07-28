using Configuration;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class ReportCreationTests
    {
        [TestMethod]
        public async Task ReportCreator_Should_Save_Report()
        {
            Environment.SetEnvironmentVariable("Trade", "PowerTrade");

            var gmtTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            var reportName = "PowerPosition_" + gmtTime.ToString("yyyyMMdd_HHmm") + ".csv";
            var testExePath = ConfigurationManager.TradeReportsPath;
            var reportPath = Path.Combine(testExePath, reportName);
            var mockFetcher = new Mock<ITradesFetcher>();            
            var mockCalculator = new Mock<ITradeVolumeCalculator>();
            var mockLogger = new Mock<IServiceLogger>();

            mockFetcher
                .Setup(f => f.GetTradesAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(() =>
                new List<ITrade>
                {
                    new DummyTrade(),
                    new DummyTrade()
                });
            mockCalculator
                .Setup(c => c.CalculateAggregateVolumes(It.IsAny<IEnumerable<ITrade>>()))
                .Returns(() => new Dictionary<string, double>());

            var reportCreator = new ReportCreator(mockFetcher.Object, mockCalculator.Object);

            await reportCreator.CreateTradeVolumeReportAsync(DateTime.Now, testExePath);

            if (!File.Exists(reportPath))
            {
                Assert.Fail();
            }
        }
    }
}
