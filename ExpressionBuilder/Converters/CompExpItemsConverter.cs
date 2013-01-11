using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ExpressionBuilder.Expressions;

namespace ExpressionBuilder.Converters
{
    public class CompExpItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var cExp = value as ComparisionExpression;
            if (cExp != null)
            {
                return new object[] { cExp.Left, cExp.Right };
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
