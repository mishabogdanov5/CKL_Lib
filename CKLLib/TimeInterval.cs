using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class TimeInterval : IComparable<TimeInterval>
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }

        public double Duration
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        public TimeInterval(double startTime, double endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool Equals(object? obj)
        {
            TimeInterval? interval = obj as TimeInterval;
            if (interval == null) return false;

            return StartTime == interval.StartTime && EndTime == interval.EndTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartTime, EndTime);
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

        public static readonly TimeInterval ZERO = new TimeInterval(0, 0);
    }
}
