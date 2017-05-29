using System;
using System.Globalization;
using Xamarin.Forms;

namespace Prospects.Cross.Converters
{
	public class VoidToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (System.Convert.ToBoolean(parameter))
			{
				if (value == null ||
					string.IsNullOrEmpty(value.ToString()) ||
					string.IsNullOrWhiteSpace(value.ToString()))
					return true;
				else
					return false;
			}
			else
			{
				if (value == null ||
					string.IsNullOrEmpty(value.ToString()) ||
					string.IsNullOrWhiteSpace(value.ToString()))
					return false;
				else
					return true;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
