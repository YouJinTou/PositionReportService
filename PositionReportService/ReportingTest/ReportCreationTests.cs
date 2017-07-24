﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReportingTest
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
        public void ReportCreator_Should_Save_Report()
        {
            Environment.SetEnvironmentVariable("Trade", "PowerTrade");

            DateTime gmtTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            string reportName = "PowerPosition_" + gmtTime.ToString("yyyyMMdd_HHmm") + ".csv";
            string testExePath = AppDomain.CurrentDomain.BaseDirectory;
            string reportPath = Path.Combine(testExePath, reportName);
                
            Task.Run(() => ReportCreator.CreateTradeVolumeReportAsync(DateTime.Now, testExePath, new PowerService(), TradeType.PowerTrade));
        }
    }
}
