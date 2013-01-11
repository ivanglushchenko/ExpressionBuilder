using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ExpressionBuilder.Demo
{
    public class ValuesConverter : IValueConverter
    {
        public ValuesConverter()
        {
            _values["Gender"] = new List<string>() { "Male", "Female" };
        }

        private Dictionary<string, List<string>> _values = new Dictionary<string,List<string>>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && _values.ContainsKey(value.ToString()))
            {
                return _values[value.ToString()];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
