using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CKLLib;

namespace CKLDrawing
{
    internal class TimeOx : Canvas
    {
        public List<Section> Sections { get; }
        public TimeSpan DelCoast { get => _delCoast; set { } }
        public TimeInterval GlobalInterval { get;  }

        private TimeSpan _delCoast;

        public TimeOx(TimeInterval globalInterval, TimeSpan delCoast) 
        {
            _delCoast = delCoast;
            GlobalInterval = globalInterval;
        }
    }
}
