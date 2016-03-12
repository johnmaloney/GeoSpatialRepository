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

        private string name;
        private Queue<IMetricMessage> messages;
        private Queue<TimeStampedItem<dynamic>> datedMessages;
        private DateTime lastChange = DateTime.MinValue;

        protected readonly object syncLock = new object();
        protected readonly object warningsLock = new object();
        protected readonly object dataLock = new object();

        protected int maxWarnings = 256;
        protected int maxData = 1024;

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
                if (datedMessages == null)
                    yield break;

                foreach (var data in datedMessages)
                {
                    if (data.Created > since)
                    {
                        yield return data.Data;
                    }
                }
            }
        }

        /// <summary>
        /// Executes the entry of the Warning also notifies all listeners of the warning. One example of a
        /// Listener is the Log4Net object in the see cref="BootStrap" object.  
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="maxSame"></param>
        public void AddWarning(IMetricMessage message, int maxSame = 1)
        {
            if (message == null) return;

            try
            {
                lock (warningsLock)
                {
                    if (messages == null) messages = new Queue<IMetricMessage>();

                    // remove duplicates
                    if (messages.Count(w => w.Level == message.Level && w.Message == message.Message) < maxSame)
                    {
                        string fullMessage = String.Format("({0}) {1} - {2}", name, message.Level, message.Message);

                        messages.Enqueue(message);

                        ServiceMetricEventHub.DispatchToListeners(
                            message.Level, fullMessage, message.Code, message.Exception);

                        message.Exception = null;

                        lastChange = DateTime.Now;
                    }

                    // keep the list to a reasonable size
                    while (messages.Count > maxWarnings)
                        messages.Dequeue();
                }
            }
            // generating exceptions when adding warnings must be prevented because it will likely cause nexted finally blocks to exit prematurely
            catch { }
        }

        /// <summary>
        /// Gets all messages in the queue since the provided date.
        /// </summary>
        /// <param name="since"></param>
        /// <returns></returns>
        public IEnumerable<IMetricMessage> GetMessagesSince(DateTime since)
        {
            lock (warningsLock)
            {
                if (messages == null)
                    yield break;

                foreach (var warning in messages.Where(w => w.Created > since))
                    yield return warning;
            }
        }
        
        public void Clear()
        {
            lock (warningsLock)
            {
                messages.Clear();
            }

            lock (dataLock)
            {
                datedMessages.Clear();
            }
        }

        #endregion
    }
}
