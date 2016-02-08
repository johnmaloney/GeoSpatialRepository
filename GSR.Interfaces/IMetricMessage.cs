using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSR.Interfaces.Types;

namespace GSR.Interfaces
{
    public interface IMetricMessage
    {
        DateTime Created { get; set; }
        LogLevel Level { get; set; }
        string Message { get; set; }
        string Code { get; set; }
        Exception Exception { get; set; }
    }
}