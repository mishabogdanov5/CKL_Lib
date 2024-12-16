using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class CKL
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public TimeInterval GlobalInterval { get; internal set; }
        public HashSet<object> Source { get; private set; }
        public HashSet<RelationItem> Relation { get; private set; }

        public CKL(string filePath)
        {
            FilePath = filePath;
            Name = string.Empty;
            GlobalInterval = new TimeInterval(DateTime.Now, DateTime.Now);
            Source = new HashSet<object>();
            Relation = new HashSet<RelationItem>();
        }

        public CKL(string filePath, string name, TimeInterval timeInterval, HashSet<object> source, HashSet<RelationItem> relation)
        {
            FilePath = filePath;
            Name = name;
            GlobalInterval = timeInterval;
            Source = source;
            Relation = relation;
        }
    }
}
