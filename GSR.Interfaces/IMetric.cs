using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSR.Interfaces.Types;

namespace GSR.Interfaces
{
    public interface IMetric
    {
        string Name { get; }
        object Value { get; }
        string GroupName { set; }
        IncrementType MetricType { get; }

        IEnumerable<dynamic> GetDataSince(DateTime since);
        IEnumerable<IMetricMessage> GetMessagesSince(DateTime since);
    }
}
