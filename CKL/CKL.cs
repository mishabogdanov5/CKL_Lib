
using System.Runtime.InteropServices.ObjectiveC;

namespace CKL
{
    public class CKL
    {
        public string Name { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public HashSet<object> A { get; private set; }
        public HashSet<object>? B { get; private set; }
        public HashSet<RelationItem> Relation { get; private set; }

        public CKL() 
        {
            Name = string.Empty;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MaxValue;
            A = new HashSet<object>();
            B = new HashSet<object>();
            RelationItem = new HashSet<RelationItem>();
        }
        
        public CKL(string name, DateTime startTime, DateTime endTime, HashSet<object> a, HashSet<RelationItem> relation) 
        {
            Name = string.Empty;
            StartTime = startTime;
            EndTime = endTime;
            A = a;
            Relation = relation;
        }

        public CKL(string name, DateTime startTime, DateTime endTime, HashSet<object> a, HashSet<object>? b, HashSet<RelationItem> relation)
        {
            Name = string.Empty;
            StartTime = startTime;
            EndTime = endTime;
            A = a;
            B = b;
            Relation = relation;
        }
    }

}
