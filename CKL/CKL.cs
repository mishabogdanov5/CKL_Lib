
namespace CKLLib
{
    public class CKL
    { 
        public string Name { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public HashSet<object> Source { get; private set; }
        public HashSet<RelationItem> Relation { get; private set; }

        public CKL()
        {
            Name = string.Empty;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MaxValue;
            Source = new HashSet<object>();
            Relation = new HashSet<RelationItem>();
        }

        public CKL(string name, DateTime startTime, DateTime endTime, HashSet<object> source, HashSet<RelationItem> relation)
        {
            Name = string.Empty;
            StartTime = startTime;
            EndTime = endTime;
            Source = source;
            Relation = relation;
        }
    }

}
