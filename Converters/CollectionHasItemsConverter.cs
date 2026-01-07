using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFindr.Converters
{
    public class CollectionHasItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (value is string)
                return !string.IsNullOrWhiteSpace((string)value);

            if (value is IEnumerable enumerable)
            {
                foreach (var _ in enumerable)
                    return true;
                return false;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
