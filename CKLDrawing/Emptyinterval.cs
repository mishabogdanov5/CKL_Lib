using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CKLLib;

namespace CKLDrawing
{
    internal class Emptyinterval: Button
    {
        public TimeInterval Duration { get => _duration; private set { } }
		new public Chain? Parent { get; }
		
        private TimeInterval _duration;
        
        private void SetDefault() 
        {
            Background = Constants.Colors.EMPTY_INTERVAL_COLOR;
            Height = Constants.Dimentions.LINE_HEIGHT;
            BorderThickness = new Thickness(0,0,0,0);
        }

        public Emptyinterval(TimeInterval duraction) : base() 
        {
            _duration = duraction;
            SetDefault();
        }
    }
}
