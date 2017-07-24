using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class WindowsEventLogStrategy : LogStrategyBase, ILogStrategy
    {
        public void LogEvent(ServiceEvent serviceEvent, string message)
        {
            throw new NotImplementedException();
        }

        internal override void OnApiCallFailed()
        {
            throw new NotImplementedException();
        }

        internal override void OnGenerationIntervalChanged()
        {
            throw new NotImplementedException();
        }

        internal override void OnInvalidTradeTypeReceived()
        {
            throw new NotImplementedException();
        }

        internal override void OnMaxApiCallsExceeded()
        {
            throw new NotImplementedException();
        }

        internal override void OnReportCreatedSuccessfully()
        {
            throw new NotImplementedException();
        }

        internal override void OnServiceInitialized()
        {
            throw new NotImplementedException();
        }

        internal override void OnServiceStopped()
        {
            throw new NotImplementedException();
        }

        internal override void OnSleeping()
        {
            throw new NotImplementedException();
        }

        internal override void OnVolumeCalculationFailed()
        {
            throw new NotImplementedException();
        }
    }
}
