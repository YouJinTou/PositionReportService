using Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;
using System;

namespace Testing
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void ConfigurationManager_Should_Correctly_Parse_Config_File()
        {
            Assert.AreEqual(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.TradeReportsPath);
            Assert.AreEqual(new TimeSpan(0, 1, 0), ConfigurationManager.GenerationIntervalInMinutes);
            Assert.AreEqual(TradeType.PowerTrade, ConfigurationManager.TradeType);
            Assert.AreEqual(ServiceType.PowerService, ConfigurationManager.ServiceType);
        }
    }
}
