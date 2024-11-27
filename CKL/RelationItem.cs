using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class RelationItem
    {
        public object[] Value { get; private set; }
        public DateTime[] StartTimes { get; private set; }
        public DateTime[] EndTimes { get; private set; }

        public RelationItem()
        {
            Value = new object[2];
            StartTimes = Array.Empty<DateTime>();
            EndTimes = Array.Empty<DateTime>();
        }

        public RelationItem(object[] value, DateTime[] startTimes, DateTime[] endTimes)
        {
            Value = value;
            StartTimes = startTimes;
            EndTimes = endTimes;
        }
    }
}
