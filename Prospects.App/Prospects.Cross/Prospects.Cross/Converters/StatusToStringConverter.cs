using System;
using System.Globalization;
using Xamarin.Forms;

namespace Prospects.Cross.Converters
{
    public class StatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (System.Convert.ToInt32(value))
            {
                case 0:
                    return "Pending"; 
				case 1:
					return "Approved";
				case 2:
					return "Accepted";
				case 3:
					return "Rejected";
				case 4:
					return "Disabled";
                default:
                    return "";
			}
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
