﻿using System.Windows;
using System.Windows.Controls;
using CKLLib;

namespace CKLDrawing
{
    public class Chain : StackPanel
    {
        public RelationItem Item { get => _item; }
        public List<Interval> Intervals { get => _intervals; }
        public List<Emptyinterval> Emptyintervals { get => _emptyintervals; }
        public ValueBox Box { get => _box;  }

        private RelationItem _item;
        private TimeInterval _interval;
        private List<Interval> _intervals;
        private List<Emptyinterval> _emptyintervals;
        private ValueBox _box;

        public Chain(RelationItem item, TimeInterval interval, double width) : base()
        {
            _item = item;
            _interval = interval;
            Width = width;
            SetUp();
        }

        private void SetUp()
        {
            Background = Constants.DefaultColors.CKL_BACKGROUND;
            Orientation = Orientation.Horizontal;
			Height = Constants.Dimentions.CHAIN_HEIGHT;
            Margin = Constants.Dimentions.CHAIN_MARGIN;
            
            _intervals = new List<Interval>();
			_emptyintervals = new List<Emptyinterval>();
            SetValue();
            FillIntervals();
        }

        private void SetValue()
        {
            _box = new ValueBox(_item.Value);
            _box.Margin = new Thickness(0,0,0,0);

            Children.Add(_box);

            _width = Width - (Constants.Dimentions.VALUE_BOX_WIDTH + 
                Constants.Dimentions.OX_FREE_INTERVAL);
        }

        private double _width;

        private void FillIntervals()
        {

            List<Pair> pairs = new List<Pair>();

            foreach (TimeInterval interval in _item.Intervals)
            {
                pairs.Add(GetCoordinatesFromTimeInterval(interval));
            }

            Pair[] coordinates = pairs.ToArray();

            double rectWidth = 0;
            double lineWidth = 0;
            Emptyinterval currentLine;
            Interval currentRect;

            for (int i = 0; i < coordinates.Length; i++)
            {
                if (i == coordinates.Length - 1)
                {
                    rectWidth = _width * (Convert.ToDouble(coordinates[i].SecondValue) -
                        Convert.ToDouble(coordinates[i].FirstValue));

                    lineWidth = _width * (1 - Convert.ToDouble(coordinates[i].SecondValue)) 
                        + Constants.Dimentions.SECTION_WIDTH;

					currentRect = new Interval(_item.Intervals[i]);
					currentLine = new Emptyinterval(new TimeInterval(_item.Intervals[i].EndTime, _interval.EndTime));
				}

                else
                {
                    rectWidth = _width * (Convert.ToDouble(coordinates[i].SecondValue) -
                        Convert.ToDouble(coordinates[i].FirstValue));

                    lineWidth = _width * (Convert.ToDouble(coordinates[i + 1].FirstValue) -
                        Convert.ToDouble(coordinates[i].SecondValue));

					currentRect = new Interval(_item.Intervals[i]);
					currentLine = new Emptyinterval(new TimeInterval(_item.Intervals[i].EndTime, 
                        _item.Intervals[i + 1].StartTime));
				}

                if (i == 0)
                {
                    double starterLineWidth = _width * (Convert.ToDouble(coordinates[i].FirstValue));
                    Emptyinterval line = new Emptyinterval(new TimeInterval(_interval.StartTime, _item.Intervals[i].StartTime));
                    LineSetUp(line, starterLineWidth);
                    AddEmptyInterval(line);
                }

                RectSetUp(currentRect, rectWidth);
                LineSetUp(currentLine, lineWidth);

                AddInterval(currentRect);
                AddEmptyInterval(currentLine);
            }
        }

        private void AddEmptyInterval(Emptyinterval line) 
        {
            Children.Add(line);
            _emptyintervals.Add(line);
        }

        private void AddInterval(Interval rect) 
        {
            Children.Add(rect);
            _intervals.Add(rect);
        }

        private void RectSetUp(Interval interval, double width) 
        {
            interval.Width = width;
            interval.Height = Height;
        }

        private void LineSetUp(Emptyinterval line, double width)
        {
            line.Width = width;
        }

        private Pair GetCoordinatesFromTimeInterval(TimeInterval interval) 
        {
            TimeSpan vectorBegin = interval.StartTime - _interval.StartTime;
            TimeSpan vectorEnd = interval.EndTime - _interval.StartTime;
 
            double start = vectorBegin.TotalNanoseconds / _interval.Duration.TotalNanoseconds;
            double end = vectorEnd.TotalNanoseconds / _interval.Duration.TotalNanoseconds;

            return new Pair(start, end);
        }
    }
}
