using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFindr.Converters
{
    public class BooleanToYesNoConverter  : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is null)
                return "N/A";
            if (value is bool b)
            {
                return b ? "Yes" : "No";
            }
            return "N/A";
        }
        public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return s.Equals("Yes", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
