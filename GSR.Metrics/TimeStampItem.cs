using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSR.Metrics
{
    public class TimeStampedItem<T>
    {
        public DateTime Created { get; set; }
        public T Data { get; set; }

        public TimeStampedItem(T data)
        {
            Created = DateTime.Now;
            Data = data;
        }
    }
}
