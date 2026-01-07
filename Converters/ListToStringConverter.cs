using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Microsoft.Maui.Controls;

namespace GameFindr.Converters
{
    public class ListToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return "N/A";

            if (value is string s)
                return string.IsNullOrWhiteSpace(s) ? "N/A" : s;

            if (value is IEnumerable items)
            {
                string? propName = parameter as string;

                var parts = items.Cast<object?>()
                    .Select(item =>
                    {
                        if (item is null) return null;
                        if (propName is null) return item.ToString();
                        var prop = item.GetType().GetProperty(propName);
                        return prop?.GetValue(item)?.ToString() ?? item.ToString();
                    })
                    .Where(x => !string.IsNullOrWhiteSpace(x));

                var result = string.Join(", ", parts);
                return string.IsNullOrEmpty(result) ? "N/A" : result;
            }

            return value.ToString() ?? "N/A";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
