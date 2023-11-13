using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Data;

namespace IPS_CALC.Converters
{
    public class GeneralEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                var NameObj = value.GetType().GetEnumName(value);
                var DisplayName = value.GetType().GetTypeInfo().GetDeclaredField(NameObj).GetCustomAttribute<DisplayAttribute>().Name;
                return DisplayName;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
