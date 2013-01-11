using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionBuilder.Expressions.Converters
{
    public abstract class Converter<T>
    {
        #region Methods

        public abstract T Convert(ComparisionExpression expression);
        public abstract T Convert(LogicalExpression expression);
        public abstract T Convert(LeftValueExpression expression);
        public abstract T Convert(RightValueExpression expression);

        public T Convert(Expression expression)
        {
            return expression.Convert(this);
        }

        #endregion Methods
    }
}
