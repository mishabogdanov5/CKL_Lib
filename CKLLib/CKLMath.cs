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

                TimeInterval intervalsConjuction = new TimeInterval(st, et);

                HashSet<RelationItem> items = new HashSet<RelationItem>();
                List<TimeInterval> timeIntervals = new List<TimeInterval>();

                double newSTime;
                double newETime;

                foreach (RelationItem item in ckl.Relation)
                {
                    timeIntervals.Clear();

                    for (int i = 0; i < item.Intervals.Count; i++)
                    {
                        newSTime = item.Intervals[i].StartTime >= st ?
                            item.Intervals[i].StartTime : st;

                        newETime = item.Intervals[i].EndTime >=st ?
                            et : item.Intervals[i].EndTime;

                        if (newSTime < newETime) timeIntervals.Add(
                            new TimeInterval(newSTime, newETime));
                    }

                    if (timeIntervals.Count > 0) items.Add(new RelationItem(item.Value, timeIntervals));
                }

                return new CKL(ckl.FilePath, ckl.Name, newInterval, ckl.Dimention, ckl.Source, items);
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

                return new CKL(ckl.FilePath, ckl.Name, ckl.GlobalInterval, ckl.Dimention, newSource, newRelation);
            }

            public static CKL SourceExpansion(CKL ckl, IEnumerable<object> expansion)
            {
                if (ckl == null) throw new ArgumentNullException("CKL object con not be null");

                HashSet<object> newSource = ckl.Source.Concat(expansion).ToHashSet();

                return new CKL(ckl.FilePath, ckl.Name, ckl.GlobalInterval, ckl.Dimention, newSource, ckl.Relation);
            }

            //CKL source operations

            private static void TryThrowBinaryExceptions(CKL ckl1, CKL ckl2) 
            {
				if (ckl1 == null) throw new ArgumentNullException();
				if (ckl2 == null) throw new ArgumentNullException();

				if (!ckl1.GlobalInterval.Equals(ckl2.GlobalInterval)) throw new ArgumentException();
				if (!ckl1.Source.SetEquals(ckl2.Source)) throw new ArgumentException();
			}

            private static TimeInterval IntervalConjunction(TimeInterval i1, TimeInterval i2) 
            {
                if (i1.StartTime >= i2.EndTime || i2.StartTime >= i1.EndTime)
                    return TimeInterval.ZERO;
                return new TimeInterval
                    (
                        i1.StartTime >= i2.StartTime ? i1.StartTime : i2.StartTime,
                        i1.EndTime >= i2.EndTime ? i2.EndTime : i1.EndTime
                    );
			}

            private static List<TimeInterval> IntervalsDisjunction(TimeInterval i1, TimeInterval i2) 
            {
                if (i1.StartTime >= i2.EndTime || i2.StartTime >= i1.EndTime)
                    return new List<TimeInterval> { i1, i2 };
                
                return new List<TimeInterval> { new TimeInterval
                    (
                        i1.StartTime <= i2.StartTime ? i1.StartTime : i2.StartTime,
                        i1.EndTime <= i2.EndTime ? i2.EndTime : i1.EndTime
                    ) 
                };
            }

            public static CKL Intersection(CKL ckl1, CKL ckl2) 
            {
                TryThrowBinaryExceptions(ckl1, ckl2);

                return new CKL();
            }

            public static CKL Union(CKL ckl1, CKL ckl2)
            {
                

                HashSet<RelationItem> relation = new HashSet<RelationItem>();


                return new CKL();

            }
        }
    }
}
