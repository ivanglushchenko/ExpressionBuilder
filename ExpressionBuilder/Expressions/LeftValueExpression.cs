using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ExpressionBuilder.Expressions.Converters;

namespace ExpressionBuilder.Expressions
{
    public partial class LeftValueExpression : ValueExpression
    {
        #region .ctors

        public LeftValueExpression()
        {
        }

        #endregion .ctors

        #region Methods

        public override T Convert<T>(Converter<T> converter)
        {
            return converter.Convert(this);
        }

        #endregion Methods
    }
}
