using Configuration;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class ReportCreationTests
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
        public async Task ReportCreator_Should_Save_Report()
        {
            Environment.SetEnvironmentVariable("Trade", "PowerTrade");

            DateTime gmtTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            string reportName = "PowerPosition_" + gmtTime.ToString("yyyyMMdd_HHmm") + ".csv";
            string testExePath = ConfigurationManager.TradeReportsPath;
            string reportPath = Path.Combine(testExePath, reportName);

            await ReportCreator.CreateTradeVolumeReportAsync(DateTime.Now, testExePath, new PowerService(), TradeType.PowerTrade, new ServiceLogger(LogStrategy.Console));

            if (!File.Exists(reportPath))
            {
                Assert.Fail();
            }
        }
    }
}
