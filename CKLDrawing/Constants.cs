using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace CKLDrawing
{
    internal static class Constants
    {
        public static class Colors 
        {
            public static readonly Brush INTERVAL_ITEM_COLOR = new SolidColorBrush(Color.FromRgb(119, 139, 235));
            public static readonly Brush INTERVAL_ITEM_ACTIVE_COLOR = new SolidColorBrush(Color.FromRgb(51, 76, 190));
            public static readonly Brush INTERVAL_ITEM_BORDER_COLOR = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            public static readonly Brush EMPTY_INTERVAL_COLOR = new SolidColorBrush(Color.FromRgb(235, 134, 134));
            public static readonly Brush CHAIN_COLOR = new SolidColorBrush(Color.FromRgb(68, 68, 68));
        }

        public static class Dimentions 
        {
            public static readonly double LINE_HEIGHT = 8;
            public static readonly double INTERVAL_BORDER_SIZE = 3;
        }
    }
}
