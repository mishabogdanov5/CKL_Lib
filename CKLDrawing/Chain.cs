using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CKLLib;

namespace CKLDrawing
{
    internal class Chain : StackPanel
    {
        public RelationItem Item { get => _item; }
        public TimeInterval GlobalInterval { get => _interval; }
        public List<Interval> Intervals { get; }
        public List<Emptyinterval> Emptyintervals { get; }

        private RelationItem _item;
        private TimeInterval _interval;

        public Chain(RelationItem item) : base()
        {
            _item = item;
        }

        private void SetUp()
        {
            Background = Constants.Colors.CHAIN_COLOR;
            Orientation = Orientation.Horizontal;

            FillIntervals();
        }

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
                    rectWidth = Width * (Convert.ToDouble(coordinates[i].SecondValue) -
                        Convert.ToDouble(coordinates[i].FirstValue));

                    lineWidth = Width * (1 - Convert.ToDouble(coordinates[i].FirstValue));

					currentRect = new Interval(_item.Intervals[i]);
					currentLine = new Emptyinterval(new TimeInterval(_item.Intervals[i].EndTime, _interval.EndTime));
				}

                else
                {
                    rectWidth = Width * (Convert.ToDouble(coordinates[i].SecondValue) -
                        Convert.ToDouble(coordinates[i].FirstValue));

                    lineWidth = Width * (Convert.ToDouble(coordinates[i + 1].FirstValue) -
                        Convert.ToDouble(coordinates[i].SecondValue));

					currentRect = new Interval(_item.Intervals[i]);
					currentLine = new Emptyinterval(new TimeInterval(_item.Intervals[i].EndTime, 
                        _item.Intervals[i + 1].StartTime));
				}

                if (i == 0)
                {
                    double starterLineWidth = Width * (Convert.ToDouble(coordinates[i].FirstValue));
                    Emptyinterval line = new Emptyinterval(new TimeInterval(_interval.StartTime, _item.Intervals[i].StartTime));
                    LineSetUp(line, starterLineWidth, Height/2);
                    AddEmptyInterval(line);
                }

                RectSetUp(currentRect, rectWidth);
                LineSetUp(currentLine, lineWidth, Height/2);

                AddInterval(currentRect);
                AddEmptyInterval(currentLine);
            }
        }

        private void AddEmptyInterval(Emptyinterval line) 
        {
            Children.Add(line);
            Emptyintervals.Add(line);
        }

        private void AddInterval(Interval rect) 
        {
            Children.Add(rect);
            Intervals.Add(rect);
        }

        private void RectSetUp(Interval interval, double width) 
        {
            interval.Width = width;
            interval.Height = Height;
        }

        private void LineSetUp(Emptyinterval line, double width, double topMargin)
        {
            line.Width = width;
            line.Margin = new Thickness(0, topMargin, 0, 0);
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
