using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public static class ServiceLogger
    {
        public static void LogEvent(ServiceEvent serviceEvent, ILogStrategy logMethod, string message = null)
        {
            logMethod.LogEvent(serviceEvent, message);
        }
    }
}
