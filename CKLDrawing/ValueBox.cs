using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CKLLib;
using System.Windows.Automation.Provider;
namespace CKLDrawing
{
	public class ValueBox: Button
	{
		public Chain? CurrentChain { get => _chain; }
		public RelationItem? Item { get => _item; }
		public object Info { get => _info; }

		private Chain _chain;
		private RelationItem _item;
		private object _info;
		public ValueBox(RelationItem item, Chain chain) : base() 
		{
			_item = item;
			_chain = chain;
			
			SetUp();
		}

		public ValueBox(object info) : base() 
		{
			_info = info;
			SetUp();
		}

		private void SetUp() 
		{
			if (_item != null) Content = _item.Value.ToString();
			else Content = _info.ToString();
			
			Width = Constants.Dimentions.VALUE_BOX_WIDTH;
			HorizontalContentAlignment = HorizontalAlignment.Center;
			VerticalContentAlignment = VerticalAlignment.Center;
			Height = Constants.Dimentions.CHAIN_HEIGHT;
			Background = Constants.DefaultColors.CKL_BACKGROUND;
			Foreground = Constants.DefaultColors.VALUE_COLOR;
			BorderThickness = new Thickness(Constants.Dimentions.SECTION_WIDTH);
			BorderBrush = Constants.DefaultColors.INTERVAL_ITEM_BORDER_COLOR;
			Margin = Constants.Dimentions.CHAIN_MARGIN;
		}
	}
}
