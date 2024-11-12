using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKLLib
{
    public class RelationItem 
    {
        public string[] Items { get; internal set; }
        public DateTime[] StartTimes { get; internal set; }
        public DateTime[] EndTimes { get; internal set; }

        public RelationItem(string[] items, DateTime[] startTimes, DateTime[] endTimes) 
        {
            Items = items;
            StartTimes = startTimes;
            EndTimes = endTimes;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not RelationItem) return false;
            if (obj == null) return false;
            
            RelationItem item = obj as RelationItem;

            return (item.Items.SequenceEqual(Items) && item.StartTimes.SequenceEqual(StartTimes) &&
                item.EndTimes.SequenceEqual(EndTimes));
        }
    }
}
