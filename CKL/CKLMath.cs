using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    namespace Operations 
    {
        public static class CKLMath
        {
            //Time operations
            public static CKL TimeTransform(CKL ckl, DateTime newStartTime, DateTime newEndTime) 
            {
                if (newStartTime.CompareTo(newEndTime) > 0) 
                    throw new ArgumentException("Start time must be less than end time.");

                if (ckl == null)
                    throw new ArgumentNullException("CKL object can not be null.");

                DateTime t1 = ckl.StartTime.CompareTo(newStartTime) >= 0 ? ckl.StartTime : newStartTime;
                DateTime t2 = ckl.EndTime.CompareTo(newEndTime) >= 0 ? newEndTime : ckl.EndTime;

                if (t1.CompareTo(t2) >= 0) 
                {
                    return new CKL() { Name = ckl.Name, StartTime = newStartTime, EndTime = newEndTime, 
                        Source = ckl.Source};
                }

                HashSet<RelationItem> relation = [];

                List<DateTime> startTimes;
                List<DateTime> endTimes;
                
                foreach (RelationItem item in ckl.Relation)
                {
                    startTimes = [];
                    endTimes = [];

                    if (item.StartTimes.Length != item.EndTimes.Length)
                        throw new ArgumentOutOfRangeException("Relation Start Times Length < Relation end Times Length");

                    for (int i = 0; i < item.StartTimes.Length; i++) 
                    {
                        if (t1.CompareTo(item.EndTimes[i]) > 0 ||
                            t2.CompareTo(item.StartTimes[i]) < 0) continue;

                        DateTime st = t1.CompareTo(item.StartTimes[i]) >= 0 ? t1 : item.StartTimes[i];
                        DateTime et = t2.CompareTo(item.EndTimes[i]) >= 0 ? item.EndTimes[i] : t2;

                        startTimes.Add(st);
                        endTimes.Add(et);
                    }

                    if (startTimes.Count > 0) 
                    {
                        relation.Add(new RelationItem(item.Items, startTimes.ToArray(), endTimes.ToArray()));
                    }
                }

                return new CKL(ckl.Name, newStartTime, newEndTime, ckl.Source, relation);
            }

            public static CKL TimeDisjuction(CKL ckl, DateTime startTime, DateTime endTime) 
            {
                if (ckl == null)
                    throw new ArgumentNullException("CKL object can not be null.");
                DateTime newStartTime = ckl.StartTime.CompareTo(startTime) >= 0 ? startTime : ckl.StartTime;
                DateTime newEndTime = ckl.EndTime.CompareTo(endTime) >= 0 ? ckl.EndTime : endTime;

                return TimeTransform(ckl, newStartTime, newEndTime);
            }

            public static CKL TimeConjunction(CKL ckl, DateTime startTime, DateTime endTime) 
            {
                if (ckl == null)
                    throw new ArgumentNullException("CKL object can not be null.");
                DateTime newStartTime = ckl.StartTime.CompareTo(startTime) >= 0 ? ckl.StartTime : startTime;
                DateTime newEndTime = ckl.EndTime.CompareTo(endTime) >= 0 ? endTime : ckl.EndTime;

                return TimeTransform(ckl, newStartTime, newEndTime);
            } 

            public static CKL LeftTimePrecedence(CKL ckl, DateTime intervalStartTime, DateTime intervalEndTime, double t)
            {
                //
            }



            //
        }
    }
}
