using System.Text;

namespace CKLLib
{
    public class CKL {
        public string Name { get; set; }
        public DateTime StartTime { get; internal set; }
        public DateTime EndTime { get; internal set; }
        public HashSet<string>[] Source { get; internal set; } 
        public HashSet<RelationItem> Relation { get; internal set; }

        public CKL(DateTime startTime, DateTime endTime, 
            HashSet<string>[] source, HashSet<RelationItem> realtion) 
        {
            Name = string.Empty;
            StartTime = startTime;
            EndTime = endTime;
            Source = source;
            Relation = realtion;
        }

        public CKL(string name, DateTime startTime,
            DateTime endTime, HashSet<string>[] source, HashSet<RelationItem> realtion)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Source = source;
            Relation = realtion;
        }

        public CKL() 
        {
            Name = string.Empty;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MaxValue;
            Source = [];
            Relation = [];
        }

        public override bool Equals(object? obj)
        {
            if (obj is not CKL || obj == null) return false;
            
            CKL item = obj as CKL;

            return (item.Name.Equals(Name) && item.StartTime.Equals(item.EndTime) &&
                item.EndTime.Equals(EndTime) && item.Source.SequenceEqual(Source) &&
                item.Relation.SetEquals(Relation));
        }
    }
}
