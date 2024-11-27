using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKL
{
    namespace Operations 
    {
        public static class CKLMath
        {

            public static CKL TimeTransform (CKL ckl, DateTime newStartTime, DateTime newEndTime) 
            {
                if (ckl == null) throw new ArgumentNullException("CKL object con not be null");
              
                DateTime st = newStartTime.CompareTo(ckl.StartTime) >= 0 ? newStartTime : ckl.StartTime;
                DateTime et = newEndTime.CompareTo(ckl.EndTime) >= 0 ? ckl.EndTime : newEndTime;

                if (newStartTime.CompareTo(newEndTime) >= 0 || st.CompareTo(et) >= 0) return 
                        new CKL() { Name = ckl.Name, A = ckl.A, B = ckl.B };

                HashSet<RelationItem> items = new HashSet<RelationItem>();
                List<DateTime> sTimes = new List<DateTime>();
                List<DateTime> eTimes = new List<DateTime>();
                DateTime newSTime;
                DateTime newETime;
        
                foreach (RelationItem item in ckl.Relation) 
                {
                    if (item.EndTimes.Length != item.StartTimes.Length) 
                        throw new IndexOutOfRangeException("Relation deltas are incorrect: start_times array length should equals end_times array length");
                    
                    sTimes.Clear();
                    eTimes.Clear();
                    
                    for (int i = 0; i < item.EndTimes.Length; i++) 
                    {
                        newSTime = item.StartTimes[i].CompareTo(st) >= 0 ? item.StartTimes[i] : st;
                        newETime = item.EndTimes[i].CompareTo(et) >= 0 ? et : item.EndTimes[i];

                        if (newSTime.CompareTo(newETime) < 0)
                        {
                            sTimes.Add(newSTime);
                            eTimes.Add(newETime);
                        }
                    }

                    if (sTimes.Count > 0) items.Add( new RelationItem(item.Value, 
                        sTimes.ToArray(), eTimes.ToArray()) );
                }

                return new CKL(ckl.Name, newStartTime, newEndTime, ckl.A, ckl.B, items);
            }
        }
    }
    
}
