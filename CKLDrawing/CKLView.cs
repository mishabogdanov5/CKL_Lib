using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CKLLib;

namespace CKLDrawing
{
	public class CKLView: DockPanel
	{
		public CKL Ckl { get => _ckl;  }
		public List<Chain> Chains { get => _chains; }
		public List<Interval> SelectedIntervals { get => _selectedIntervals; }
		public TimeOx TimeScale { get => _timeScale; }
		public TimeInterval CurrentInterval { get => _currentInterval;  }
		public StackPanel ListView { get => _listView; }
		public StackPanel MainView { get => _mainView; }
		public StackPanel Content { get => _content; }
		public ScrollViewer ScrollView { get => _scrollView; }
		public TimeDimentions TimeDimention { get => _timeDimention;  }
		public double DelCoast { get => _delCoast; }
		public double ScaleMulti { get => _scaleMulti; }

		private CKL _ckl;
		private List<Chain> _chains;
		private List<Interval> _selectedIntervals;
		private TimeOx _timeScale;
		private TimeInterval _currentInterval;
		private StackPanel _listView;
		private StackPanel _mainView;
		private StackPanel _timePanel;
		private StackPanel _content;
		private ScrollViewer _scrollView;
		private TimeDimentions _timeDimention;	
		private double _delCoast;
		private double _scaleMulti;
		private double _intervalMulti;

		public CKLView(CKL ckl) : base()
		{
			_ckl = ckl;
			_delCoast = 1;
			_timeDimention = _ckl.Dimention;
			_currentInterval = new TimeInterval(_ckl.GlobalInterval.StartTime, _ckl.GlobalInterval.EndTime);
			_scaleMulti = 1;
			_intervalMulti = 1;

			SetUp();
		}

		public void ChangeDelCoast(double newDelCoast, TimeDimentions newDimention) 
		{

			if (newDelCoast < 1)
			{
				if (!newDimention.Equals(TimeDimentions.NANOSECONDS))
				{
					_timeDimention = (TimeDimentions)(int)newDimention - 1;
					_delCoast = Constants.TIME_DIMENTIONS_CONVERT[(int)_timeDimention];
					_scaleMulti *= Constants.TIME_DIMENTIONS_CONVERT[(int)_timeDimention];
					_intervalMulti = _scaleMulti;
					OnDelCoastChange();
					return;
				}
			}

			if (newDimention.Equals(_timeDimention)) 
			{
				if (newDelCoast >= Constants.TIME_DIMENTIONS_CONVERT[(int)_timeDimention])
				{
					if (!_timeDimention.Equals(TimeDimentions.WEEKS))
					{
						_delCoast = 1;
						_scaleMulti /= Constants.TIME_DIMENTIONS_CONVERT[(int)_timeDimention];
						_intervalMulti = _scaleMulti;
						_timeDimention = (TimeDimentions)(int)_timeDimention + 1;
						OnDelCoastChange();
						return;
					}
				}
			}

			ChangeScaleMulti(newDimention, newDelCoast);
			_delCoast = newDelCoast;
			_timeDimention = newDimention;

			OnDelCoastChange();
			_scaleMulti = 1;
			_intervalMulti = 1;
		}

		private void ChangeScaleMulti(TimeDimentions newDimention, double newDelCoast)
		{
			if ((int)_timeDimention > (int)newDimention)
			{
				_scaleMulti *= (_delCoast / newDelCoast);

				for (int i = (int)_timeDimention - 1; i >= (int)newDimention; i--)
				{
					_scaleMulti *= Constants.TIME_DIMENTIONS_CONVERT[i];
					_intervalMulti *= Constants.TIME_DIMENTIONS_CONVERT[i];
				}
			}
			else 
			{
				_scaleMulti *= (_delCoast / newDelCoast);

				for (int i = (int)newDimention - 1; i >= (int)_timeDimention; i--)
				{
					_scaleMulti /= Constants.TIME_DIMENTIONS_CONVERT[i];
					_intervalMulti /= Constants.TIME_DIMENTIONS_CONVERT[i];
				}
			}
		}

		private void SetUp() 
		{
			Background = Constants.DefaultColors.CKL_BACKGROUND;
			
			_chains = new List<Chain>();
			_selectedIntervals = new List<Interval>();

			_listView = new StackPanel();
			_listView.Background = Background;
			_listView.Orientation = Orientation.Vertical;

			_mainView = new StackPanel();
			_mainView.Background = Background;
			_mainView.Orientation = Orientation.Vertical;

			_content = new StackPanel();
			_content.Background = Background;
			_content.Orientation = Orientation.Horizontal;

			_scrollView = new ScrollViewer();
			_scrollView.Background = Background;
			_scrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
			_scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

			_timePanel = new StackPanel();
			_timePanel.Background = Background;
			_timePanel.Orientation = Orientation.Vertical;

			DrawOx();
			DrawChains();

			_content.Children.Add(_mainView);

			_scrollView.Content = _content;
			
			DockPanel.SetDock(_listView, Dock.Left);
			DockPanel.SetDock(_scrollView, Dock.Left);

			Children.Add(_listView);
			Children.Add(_scrollView);
		}

		private void OnDelCoastChange() 
		{
			_currentInterval.StartTime *= _intervalMulti;
			_currentInterval.EndTime *= _intervalMulti;

			_timeScale = new TimeOx(_currentInterval, _timeDimention, _delCoast);
			(_mainView.Children[0] as StackPanel).Children.Clear();
			(_mainView.Children[0] as StackPanel).Children.Add(_timeScale);

			(_listView.Children[0] as ValueBox).Content = 
				$"{_delCoast} {Constants.TIME_DIMENTIONS_STRINGS[(int)_timeDimention]}";

			SetUpChains();
		}

		private void SetUpChains() 
		{
			foreach (Chain chain in _chains) 
			{
				chain.Width *= _intervalMulti;
				foreach (Interval interval in chain.Intervals) 
				{
					interval.Width *= _scaleMulti;
				}
				foreach (Emptyinterval interval in chain.Emptyintervals) 
				{
					interval.Width *= _scaleMulti;
				}
			}
		}

		private void DrawOx()
		{

			_timeScale = new TimeOx(_ckl.GlobalInterval, _timeDimention, _delCoast);
			_timePanel.Children.Add(_timeScale);
			_mainView.Children.Add(_timePanel);

			_listView.Children.Add(new ValueBox
				($"{_delCoast} {Constants.TIME_DIMENTIONS_STRINGS[(int)_timeDimention]}")
			{
				Height = _timeScale.Height,
				Background = _timeScale.Background,
				Foreground = Constants.DefaultColors.SECTION_COLOR,
				Margin = Constants.Dimentions.TIME_OX_MARGIN
			});

		}

		private void DrawChains() 
		{
			foreach (RelationItem item in Ckl.Relation) 
			{
				Chain chain = new Chain(item, Ckl.GlobalInterval, _timeScale.Width);
				SetUpChainIntervals(chain);
				AddChain(chain);

				_listView.Children.Add(new ValueBox(item, chain));
			}
		}

		private void SetUpChainIntervals(Chain chain) 
		{
			foreach (Interval interval in chain.Intervals)
			{
				interval.Click += (object sender, RoutedEventArgs e) => 
				{
					if (interval.IsActive) _selectedIntervals.Add(interval);
					else _selectedIntervals.Remove(interval);

				};
			}
		}

		private void AddChain(Chain chain) 
		{
			_mainView.Children.Add(chain);
			_chains.Add(chain);
		}
	}
}
