using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class TimeInterval : IComparable<TimeInterval>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        public TimeInterval(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not TimeInterval) return false;

            TimeInterval? interval = obj as TimeInterval;
            if (interval == null) return false;

            return StartTime.Equals(interval.StartTime) && EndTime.Equals(interval.EndTime);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{StartTime.ToString()}; {EndTime.ToString()}]";
        }

        public int CompareTo(TimeInterval? other)
        {
            if (other == null) return 1;

            return Duration.CompareTo(other.Duration);
        }
    }
}
