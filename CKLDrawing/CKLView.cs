using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CKLLib;

namespace CKLDrawing
{
	public class CKLView: DockPanel
	{
		public CKL Ckl { get => _ckl;  }
		public List<Chain> Chains { get => _chains; }
		public TimeOx TimeScale { get => _timeScale; }
		
		private CKL _ckl;
		private List<Chain> _chains;
		private TimeOx _timeScale;

		public CKLView(CKL ckl) 
		{
			_ckl = ckl;
			
			SetUp();
		}

		private void SetUp() 
		{
			Background = Constants.DefaultColors.CKL_BACKGROUND;
			
			_chains = new List<Chain>();

			DrawOx();
			DrawChains();
		}

		private void DrawOx() 
		{
			_timeScale = new TimeOx(_ckl.GlobalInterval, GetTimeDimention());
			DockPanel.SetDock(_timeScale, Dock.Top);
			Children.Add(_timeScale);
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
			}
		}

		private void AddChain(Chain chain) 
		{
			DockPanel.SetDock(chain, Dock.Top);
			
			Children.Add(chain);
			_chains.Add(chain);
		}
	}
}
