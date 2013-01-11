using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ExpressionBuilder.Expressions.Converters;

namespace ExpressionBuilder.Expressions
{
    public partial class RightValueExpression : ValueExpression
    {
        #region .ctors

        public RightValueExpression()
        {
        }

        #endregion .ctors

        #region Methods

        public override T Convert<T>(Converter<T> converter)
        {
            return converter.Convert(this);
        }

        public static RightValueExpression Create(object val)
        {
            if (val == null)
                return new RightValueExpression() { TypeName = typeof(Object).FullName };
            return new RightValueExpression() { Value = val.ToString(), TypeName = val.GetType().FullName };
        }

        #endregion Methods
    }
}
