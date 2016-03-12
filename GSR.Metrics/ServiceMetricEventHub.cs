using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSR.Interfaces.Types;

namespace GSR.Metrics
{
    public static class ServiceMetricEventHub
    {
        public static List<Action<LogLevel, string, string, Exception>> ExceptionListeners = new List<Action<LogLevel, string, string, Exception>>();

        public static void DispatchToListeners(LogLevel level, string message, string code, Exception exception)
        {
            foreach (var listener in ExceptionListeners)
            {
                listener.Invoke(level, message, code, exception);
            }
        }
    }
}
