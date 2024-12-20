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

        public TimeDimentions Dimention { get; set; }

        public double Duration
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        public TimeInterval(double startTime, double endTime, TimeDimentions dimention)
        {
            StartTime = startTime;
            EndTime = endTime;
            Dimention = dimention;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not TimeInterval) return false;

            TimeInterval? interval = obj as TimeInterval;
            if (interval == null) return false;

            return StartTime == interval.StartTime && EndTime == interval.EndTime;
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
