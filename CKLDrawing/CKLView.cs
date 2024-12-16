using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CKLLib;

namespace CKLDrawing
{
	public class CKLView: StackPanel
	{
		public CKL Ckl { get => _ckl;  }
		public List<Chain> Chains { get => _chains; }
		public TimeOx TimeScale { get => _timeScale; }
		
		public StackPanel ListView { get => _listView; }
		public StackPanel MainView { get => _mainView; }
		public StackPanel Content { get => _content; }
		public ScrollViewer ScrollView { get => _scrollView; }

		private CKL _ckl;
		private List<Chain> _chains;
		private TimeOx _timeScale;
		private StackPanel _listView;
		private StackPanel _mainView;
		private StackPanel _content;
		private ScrollViewer _scrollView;
		
		public CKLView(CKL ckl) 
		{
			_ckl = ckl;
			
			SetUp();
		}

		private void SetUp() 
		{
			Background = Constants.DefaultColors.CKL_BACKGROUND;
			
			_chains = new List<Chain>();
			
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
			
			Children.Add(_scrollView);
		}

		private void DrawOx() 
		{
			TimeDimentions dim = GetTimeDimention();
			
			_timeScale = new TimeOx(_ckl.GlobalInterval, dim);
			_mainView.Children.Add(_timeScale);

			_listView.Children.Add(new ValueBox
				($"{_timeScale.DelCoast} {Constants.TIME_DIMENTIONS_STRINGS[(int)dim]}")
			{ Height = _timeScale.Height});
		
		}

		private TimeDimentions GetTimeDimention() 
		{
			int ind = 0;
			TimeSpan dur = _ckl.GlobalInterval.Duration;
			double current = dur.TotalNanoseconds;

			while (current > 1000 && ind < 8) 
			{
				current /= 1000;
				ind++;
			}

			return (TimeDimentions)ind;
		}

		private void DrawChains() 
		{
			foreach (RelationItem item in Ckl.Relation) 
			{
				Chain chain = new Chain(item, Ckl.GlobalInterval, _timeScale.Width);
				AddChain(chain);

				_listView.Children.Add(new ValueBox(item.Value));
			}
		}

		private void AddChain(Chain chain) 
		{
			_mainView.Children.Add(chain);
			_chains.Add(chain);
		}
	}
}
