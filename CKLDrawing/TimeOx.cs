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
using System.Windows.Input;
using System.IO.Packaging;

namespace CKLDrawing
{
    public class TimeOx : Canvas
    {
        public List<Section> Sections { get => _sections; }
        public Button Ox { get => _ox; }
        public Canvas SectionsText { get => _sectionsText; }
        public double DelCoast { get => _delCoast; }
        public TimeDimentions TimeDimention { get => _timeDimention; }

        private double _delCoast;
        private TimeInterval _interval;
        private TimeDimentions _timeDimention;
        private List<Section> _sections;
        private Button _ox;
        private Canvas _sectionsText;

        public TimeOx(TimeInterval globalInterval, TimeDimentions timeDimention, double delCoast) : base()
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

            Canvas.SetLeft(text, Constants.Dimentions.VALUE_BOX_WIDTH / 2);
            Canvas.SetTop(text, Constants.Dimentions.TIME_OX_HEIGHT / 2);

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
            Canvas.SetTop(ox, Constants.Dimentions.TIME_OX_HEIGHT / 2);

            Children.Add(ox);
            _ox = ox;
        }

        private void FillOx()
        {
            double startPos = Constants.Dimentions.FIRST_DEL_START;
            double sectionHeight = Constants.Dimentions.SECTION_HEIGHT;

            double total = _interval.Duration;

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


                SetUpSection(section, sectionHeight, startPos, val);
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

        private void SetUpSection(Section section, double height, double startPos, double value)
        {
            section.Height = height;
            Canvas.SetLeft(section, startPos);
            Canvas.SetTop(section, (Constants.Dimentions.TIME_OX_HEIGHT - height) / 2);

            section.MouseEnter += (object sender, MouseEventArgs e) =>
            {
                string s = value.ToString();

                MessageBox.Show(s);
            };
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

            string s = value.ToString();
            string res = s.Contains(',') ? s.Substring(0, s.IndexOf(',') + 1) : s;
            label.Content = res;

            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.Padding = new Thickness(0);

            Canvas.SetLeft(label, startPos);
            Canvas.SetBottom(label, Constants.Dimentions.SECTIONS_TEXT_HEIGHT / 2
                - Constants.Dimentions.SECTION_WIDTH);
            _sectionsText.Children.Add(label);

            label.Margin = new Thickness(0, 0, 0, 0);
        }

        private void SetDefaultDelCoast() { _delCoast = 1; }
    }
}
