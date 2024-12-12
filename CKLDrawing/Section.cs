using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CKLDrawing
{
    internal class Section: Button
    {
        public DateTime Value { get; private set; }

        private DateTime _value;

        public Section (DateTime value) : base()
        {
            _value = value;
        }
    }
}
