using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CKLLib
{
    public class CKL
    {
        public string FilePath { get; set; }
        public TimeInterval GlobalInterval { get; set; }
        public TimeDimentions Dimention { get; set; }
        public HashSet<Pair> Source { get; set; }
        public HashSet<RelationItem> Relation { get; set; }
        
        public CKL()
        {
            FilePath = string.Empty;
            GlobalInterval = new TimeInterval(0, 0);
            Dimention = TimeDimentions.SECONDS;
			Source = new HashSet<Pair>();
            Relation = new HashSet<RelationItem>();
        }

        public CKL(string filePath, TimeInterval timeInterval, TimeDimentions dimention ,HashSet<Pair> source, HashSet<RelationItem> relation)
        {
            FilePath = filePath;
            GlobalInterval = timeInterval;
            Dimention = dimention;
            Source = source;
            Relation = relation;

            FillRelation();
        }

        private void FillRelation() 
        {
            foreach (Pair item in Source) 
            {
                if (!Relation.Any(x => x.Value.Equals(item))) Relation.Add(new RelationItem(item, 
                    new List<TimeInterval>() { TimeInterval.ZERO } ));
            }
        }

		public override bool Equals(object? obj)
		{
            CKL? ckl = obj as CKL;
            if (ckl is null) return false;


            return GlobalInterval.Equals(ckl.GlobalInterval) && Dimention.Equals(ckl.Dimention)
                && Source.SetEquals(ckl.Source) && Relation.SetEquals(ckl.Relation);
		}

		public override int GetHashCode()
		{
            return HashCode.Combine(GlobalInterval, Dimention, Source, Relation);
		}

        public static void Save(CKL ckl) 
        {
            string s = JsonSerializer.Serialize(ckl);
            File.WriteAllText(ckl.FilePath, s);
        }

        public static CKL? GetFromFile(string path) 
        {
            return JsonSerializer.Deserialize<CKL>(File.ReadAllText(path));
        }
	}
}
