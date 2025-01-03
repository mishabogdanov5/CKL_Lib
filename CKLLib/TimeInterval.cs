using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class TimeInterval : IComparable<TimeInterval>
    {
        public double StartTime { get => _startTime; 
            set {
                if (value <= _endTime) _startTime = value;
                else _startTime = _endTime;
            } 
        }
        public double EndTime { get => _endTime; 
            set { 
                if (value >= _startTime) _endTime = value;
                else _endTime = _startTime;
            } 
        }

        private double _startTime;
        private double _endTime;

        public double Duration
        {
            get
            {
                return _endTime - _startTime;
            }
        }

        public TimeInterval(double startTime, double endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        public override bool Equals(object? obj)
        {
            TimeInterval? interval = obj as TimeInterval;
            if (interval == null) return false;

            if (interval.Duration == 0 && Duration == 0) return true;

            return _startTime == interval.StartTime && _endTime == interval.EndTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_startTime, _endTime);
        }

        public override string ToString()
        {
            return $"[{_startTime.ToString()}; {_endTime.ToString()}]";
        }

        public int CompareTo(TimeInterval? other)
        {
            if (other == null) return 1;

            return Duration.CompareTo(other.Duration);
        }

        public static readonly TimeInterval ZERO = new TimeInterval(0, 0);
    }
}
