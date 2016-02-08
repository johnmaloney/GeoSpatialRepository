using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSR.Interfaces.Types
{
    public enum IncrementType
    {
        AbsoluteValue,
        Counter,
        TimeSpan
    }

    public enum LogLevel
    {
        Message,
        Debug,
        Warning,
        Error,
        Fatal
    }
}
