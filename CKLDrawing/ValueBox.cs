using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
namespace CKLDrawing
{
	public class ValueBox: Button
	{
		public object Value { get => _value;  }
		
		private object _value;

		public ValueBox(object value) : base() 
		{
			_value = value;
			SetUp();
		}

		private void SetUp() 
		{
			Content = _value.ToString();
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
