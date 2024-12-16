using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Controls;
using System.Windows;
using CKLLib;

namespace CKLDrawing
{
    public class TimeOx : Canvas
    {
        public List<Section> Sections { get => _sections; }
        public Button Ox { get => _ox;  }
        public Canvas SectionsText { get => _sectionsText; }
        public double DelCoast { get => _delCoast; set { } }
        public TimeDimentions TimeDimention { get => _timeDimention; set { } }

        private double _delCoast;
        private TimeInterval _interval;
        private TimeDimentions _timeDimention;
        private List<Section> _sections;
        private Button _ox;
        private Canvas _sectionsText;

        public TimeOx(TimeInterval globalInterval, TimeDimentions timeDimention, double delCoast = 1) : base()
        {
            _interval = globalInterval;
            _timeDimention = timeDimention;
            _delCoast = delCoast;

            SetUp();
        }

        private void SetUp()
        {
            Background = Constants.DefaultColors.TIME_OX_COLOR;
            Height = Constants.Dimentions.TIME_OX_HEIGHT;
            Margin = Constants.Dimentions.TIME_OX_MARGIN;


            _sections = new List<Section>();

            SetUpTextCanvas();

            //SetDelCoastText();
			FillOx();
            DrawOX();
        }

        private void SetDelCoastText() 
        {
            Label text = new Label();
            text.Content = $"{_delCoast} {Constants.TIME_DIMENTIONS_STRINGS[(int)_timeDimention]}";
            text.Foreground = Constants.DefaultColors.SECTION_COLOR;
            text.FontSize = 14;
            text.Padding = new Thickness(0);

            Canvas.SetLeft(text, Constants.Dimentions.VALUE_BOX_WIDTH/2);
            Canvas.SetTop(text, Constants.Dimentions.TIME_OX_HEIGHT/2);

            Children.Add(text);
        }

        private void SetUpTextCanvas() 
        {
            _sectionsText = new Canvas();
            _sectionsText.Background = Background;
            _sectionsText.Height = Constants.Dimentions.SECTIONS_TEXT_HEIGHT;
            Canvas.SetBottom(_sectionsText, 0);

            Children.Add(_sectionsText);
        }

        private void DrawOX() 
        {
            Button ox = new Button();
            ox.Background = Constants.DefaultColors.SECTION_COLOR;
            ox.Width = Width;
			ox.Height = Constants.Dimentions.SECTION_WIDTH;
            ox.BorderThickness = new Thickness(0);
            Canvas.SetLeft(ox, /*Constants.Dimentions.VALUE_BOX_WIDTH*/ 0);
            Canvas.SetTop(ox, Constants.Dimentions.TIME_OX_HEIGHT/2);

            Children.Add(ox);
            _ox = ox;
        }

        private void FillOx() 
        {
            double startPos = Constants.Dimentions.FIRST_DEL_START;
            double sectionHeight = Constants.Dimentions.SECTION_HEIGHT;

            double total = GetTotalDimention();

            int i = 0;
            double val = 0;
            
            Section section;
            while (val <= total) 
            {
                
                section = new Section(val);

                if (i % 5 == 0) 
                {
                    sectionHeight *= 2;
                    AddText(startPos, val);
                }
                if (i % 10 == 0) 
                {
                    sectionHeight *= 2;
                    AddText(startPos, val);
                }


                SetUpSection(section, sectionHeight, startPos);
                AddSection(section);

                i++;
                val += _delCoast;
                startPos += Constants.Dimentions.DEL_WIDTH;
                sectionHeight = Constants.Dimentions.SECTION_HEIGHT;
            }
            startPos -= Constants.Dimentions.DEL_WIDTH; 

            startPos += Constants.Dimentions.OX_FREE_INTERVAL;
            Width = startPos;
        }

        private void SetUpSection(Section section, double height, double startPos) 
        {
            section.Height = height;
            Canvas.SetLeft(section, startPos);
            Canvas.SetTop(section, (Constants.Dimentions.TIME_OX_HEIGHT - height)/2);
        }

        private void AddSection(Section section)
        {
            Children.Add(section);
            _sections.Add(section);
        }

        private void AddText(double startPos, double value) 
        {
            Label label = new Label();
            label.Background = Constants.DefaultColors.TIME_OX_COLOR;
            label.FontSize = Constants.Dimentions.TEXT_SIZE;
            label.Foreground = Constants.DefaultColors.SECTION_COLOR;
            label.Content = value;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.Padding = new Thickness(0);
            label.Width = 20;

            Canvas.SetLeft(label, startPos - 20/2);
            Canvas.SetBottom(label, Constants.Dimentions.SECTIONS_TEXT_HEIGHT/2 
                - Constants.Dimentions.SECTION_WIDTH);
            _sectionsText.Children.Add(label);
		}

        private void SetDefaultDelCoast() { _delCoast = 1; }

        private double GetTotalDimention() 
        {
            double total = 0;
            
            switch (_timeDimention)
            {
                case TimeDimentions.NANOSECONDS:
                    total = _interval.Duration.TotalNanoseconds;
                    break;
                case TimeDimentions.MICROSECONDS:
                    total = _interval.Duration.TotalNanoseconds;
                    break;
                case TimeDimentions.MILLISECONDS:
                    total = _interval.Duration.TotalMilliseconds;
                    break;
                case TimeDimentions.SECONDS:
                    total = _interval.Duration.TotalSeconds;
                    break;
                case TimeDimentions.MINUTES:
                    total = _interval.Duration.TotalMinutes;
                    break;
                case TimeDimentions.HOURS:
                    total = _interval.Duration.TotalHours;
                    break;
                case TimeDimentions.DAYS:
                    total = _interval.Duration.TotalDays;
                    break;
                case TimeDimentions.MONTH:
                    total = _interval.Duration.TotalDays / 30;
                    break;
                case TimeDimentions.YEARS:
                    total = _interval.Duration.TotalDays / 365;
                    break;
                default:
                    break;
            }

            return total;
        }
    }
}
