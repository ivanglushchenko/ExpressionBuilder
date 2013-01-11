using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionBuilder.Expressions.Converters
{
    public class StringExpressionConverter : Converter<string>
    {
        #region Methods

        public override string Convert(ComparisionExpression expression)
        {
            switch (expression.Type)
            {
                case ComparisionType.Less:
                    return string.Format("({0} < {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.LessOrEqual:
                    return string.Format("({0} <= {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.Greater:
                    return string.Format("({0} > {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.GreaterOrEqual:
                    return string.Format("({0} >= {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.Equal:
                    return string.Format("({0} = {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.NotEqual:
                    return string.Format("({0} <> {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.Like:
                    return string.Format("({0} like {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                case ComparisionType.NotLike:
                    return string.Format("({0} notlike {1})", expression.Left.Convert(this), expression.Right.Convert(this));
                default:
                    throw new NotImplementedException();
            }
        }

        public override string Convert(LeftValueExpression expression)
        {
            return expression.Value;
        }

        public override string Convert(RightValueExpression expression)
        {
            switch (expression.TypeName)
            {
                case "System.String":
                    return string.Format("'{0}'", expression.Value);
                case "System.DateTime":
                case "System.Nullable`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                    return string.Format("'{0}'", expression.Value);
                default:
                    {
                        if (expression.TypeName != null)
                        {
                            var type = Type.GetType(expression.TypeName);
                            if (type != null)
                            {
                                if (type.IsEnum || (Nullable.GetUnderlyingType(type) != null && Nullable.GetUnderlyingType(type).IsEnum))
                                {
                                    return string.Format("'{0}'", expression.Value);
                                }
                            }
                        }
                        return expression.Value;
                    }
            }
        }

        public override string Convert(LogicalExpression expression)
        {
            if (expression.Operands == null || expression.Operands.Count == 0)
            {
                return string.Empty;
            }
            var body = string.Empty;
            switch (expression.Type)
            {
                case LogicalOperatorType.And:
                    body = string.Join(" and ", expression.Operands.Select(p => p.Convert(this)).ToArray());
                    break;
                case LogicalOperatorType.Or:
                    body = string.Join(" or ", expression.Operands.Select(p => p.Convert(this)).ToArray());
                    break;
                default:
                    break;
            }
            return string.Format("({0})", body);
        }

        #endregion Methods
    }
}
