﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
		
		public StackPanel ListView { get => _listView; }
		public StackPanel MainView { get => _mainView; }
		public StackPanel Content { get => _content; }
		public ScrollViewer ScrollView { get => _scrollView; }
		public TimeDimentions TimeDimention { get => _timeDimention;  }
		public double DelCoast { get => _delCoast; }

		private CKL _ckl;
		private List<Chain> _chains;
		private List<Interval> _selectedIntervals;
		private TimeOx _timeScale;
		private StackPanel _listView;
		private StackPanel _mainView;
		private StackPanel _content;
		private ScrollViewer _scrollView;
		private TimeDimentions _timeDimention;	
		private double _delCoast;

		public CKLView(CKL ckl) : base()
		{
			_ckl = ckl;
			_delCoast = 1;
			_timeDimention = _ckl.GlobalInterval.Dimention;
			
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
						_timeDimention = (TimeDimentions)(int)_timeDimention + 1;
						OnDelCoastChange();
						return;
					}
				}
			}

			_delCoast = newDelCoast;
			_timeDimention = newDimention;

			OnDelCoastChange();
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
			Children.Clear();
			SetUp();
		}

		private void DrawOx()
		{

			_timeScale = new TimeOx(_ckl.GlobalInterval, _timeDimention, _delCoast);
			_mainView.Children.Add(_timeScale);

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
