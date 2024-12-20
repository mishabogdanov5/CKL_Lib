using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    namespace Operations 
    {
        public static class CKLMath
        {
            //TimeOperations
            public static CKL TimeTransform(CKL ckl, TimeInterval newInterval)
            {
                if (ckl == null) throw new ArgumentNullException("CKL object con not be null");

                double st = newInterval.StartTime >= ckl.GlobalInterval.StartTime ? newInterval.StartTime : ckl.GlobalInterval.StartTime;
                double et = newInterval.EndTime >= ckl.GlobalInterval.EndTime ? ckl.GlobalInterval.EndTime : newInterval.EndTime;

                TimeInterval intervalsConjuction = new TimeInterval(st, et, ckl.GlobalInterval.Dimention);

                HashSet<RelationItem> items = new HashSet<RelationItem>();
                List<TimeInterval> timeIntervals = new List<TimeInterval>();

                double newSTime;
                double newETime;

                foreach (RelationItem item in ckl.Relation)
                {
                    timeIntervals.Clear();

                    for (int i = 0; i < item.Intervals.Length; i++)
                    {
                        newSTime = item.Intervals[i].StartTime >= st ?
                            item.Intervals[i].StartTime : st;

                        newETime = item.Intervals[i].EndTime >=st ?
                            et : item.Intervals[i].EndTime;

                        if (newSTime.CompareTo(newETime) < 0) timeIntervals.Add(
                            new TimeInterval(newSTime, newETime, ckl.GlobalInterval.Dimention));
                    }

                    if (timeIntervals.Count > 0) items.Add(new RelationItem(item.Value,
                        timeIntervals.ToArray()));
                }

                return new CKL(ckl.FilePath, ckl.Name, newInterval, ckl.Source, items);
            }



            //Source operations

            public static CKL SourceConstriction(CKL ckl, Func<object, bool> selector)
            {
                if (ckl == null) throw new ArgumentNullException("CKL object con not be null");

                HashSet<object> newSource = new HashSet<object>();
                HashSet<RelationItem> newRelation = new HashSet<RelationItem>();

                foreach (RelationItem item in ckl.Relation)
                {
                    if (selector(item.Value))
                    {
                        newSource.Add(item.Value);
                        newRelation.Add(item);
                    }
                }

                return new CKL(ckl.FilePath, ckl.Name, ckl.GlobalInterval, newSource, newRelation);
            }

            public static CKL SourceExpansion(CKL ckl, IEnumerable<object> expansion)
            {
                if (ckl == null) throw new ArgumentNullException("CKL object con not be null");

                HashSet<object> newSource = ckl.Source.Concat(expansion).ToHashSet();

                return new CKL(ckl.FilePath, ckl.Name, ckl.GlobalInterval, newSource, ckl.Relation);
            }

            //CKL source operations

            /*private static DateTime[] TimeMinus(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2)
            {

            }

            private static DateTime[][] TimeDisjunction(DateTime[] startTimes1, DateTime[] endTimes1, DateTime[] startTimes2, DateTime[] endTimes2)
            {

            }

            private static DateTime[]? TimeConjunction(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2)
            {

            }

            public static CKL CKLDisjunction(CKL ckl1, CKL ckl2)
            {
                if (ckl1 == null)
                    throw new ArgumentNullException($"first argument can not be null");
                if (ckl2 == null)
                    throw new ArgumentNullException($"second argument can not be null");

                
            }*/
        }
    }
}
