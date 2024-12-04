using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class RelationItem
    {
        public object Value { get; private set; }
        public TimeInterval[] Intervals { get; private set; }

        public RelationItem()
        {
            Value = new Pair(string.Empty, string.Empty);
            Intervals = Array.Empty<TimeInterval>();
        }

        public RelationItem(object value, TimeInterval[] intervals)
        {
            Value = value;

            Array.Sort(intervals, new TimeIntervalsComparer());
            Intervals = intervals;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not RelationItem) return false;

            RelationItem? item = obj as RelationItem;
            if (item == null) return false;

            return Value.Equals(item.Value) && Intervals.SequenceEqual(item.Intervals,
                new TimeIntervalEqualityComparer());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        private class TimeIntervalsComparer : IComparer<TimeInterval>
        {
            public int Compare(TimeInterval? x, TimeInterval? y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;

                return x.StartTime.CompareTo(y.StartTime);
            }
        }

        private class TimeIntervalEqualityComparer : IEqualityComparer<TimeInterval>
        {
            public bool Equals(TimeInterval? x, TimeInterval? y)
            {
                if (x == null) return false;

                return x.Equals(y);
            }

            public int GetHashCode([DisallowNull] TimeInterval obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
