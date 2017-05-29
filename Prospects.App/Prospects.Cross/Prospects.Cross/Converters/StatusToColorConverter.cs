using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prospects.Cross.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (System.Convert.ToInt32(value))
            {
                case 0:
                    return Color.FromHex("#7f8c8d");
                case 1:
                    return Color.FromHex("#2ecc71");
                case 2:
                    return Color.FromHex("#3498db");
                case 3:
                    return Color.FromHex("#e74c3c");
                case 4:
                    return Color.FromHex("#bdc3c7");
                default:
                    return Color.FromHex("#ffffff");
            } 
        } 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
