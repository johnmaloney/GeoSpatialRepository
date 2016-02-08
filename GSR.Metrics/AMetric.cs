using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSR.Interfaces;
using GSR.Interfaces.Types;

namespace GSR.Metrics
{
    public abstract class AMetric<TIncrement> : IMetric
    {
        #region Fields
        
        private Queue<TimeStampedItem<dynamic>> m_data;

        #endregion

        #region Properties

        public string GroupName { private get; set; }
        public IncrementType MetricType { get; }
        public string Name { get; }
        public object Value { get; }

        #endregion

        #region Methods

        public IEnumerable<dynamic> GetDataSince(DateTime since)
        {
            lock (dataLock)
            {
                if (m_data == null)
                    yield break;

                foreach (var data in m_data)
                {
                    if (data.Created > since || IsFullRequest(since))
                    {
                        yield return data.Data;
                    }
                }
            }
        }

        public IEnumerable<IMetricMessage> GetMessagesSince(DateTime since)
        {

        }

        public bool HasChangedSince(DateTime since)
        {

        }

        #endregion
    }
}
