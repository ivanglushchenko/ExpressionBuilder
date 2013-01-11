using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionBuilder.Expressions.Converters
{
    public class LinqExpressionConverter : Converter<System.Linq.Expressions.Expression>
    {
        #region .ctors

        public LinqExpressionConverter(Type t)
        {
            _parameter = System.Linq.Expressions.Expression.Parameter(t, "obj");
        }

        #endregion .ctors

        #region Fields

        private System.Linq.Expressions.ParameterExpression _parameter;

        #endregion Fields

        #region Properties

        public System.Linq.Expressions.ParameterExpression Parameter
        {
            get
            {
                return _parameter;
            }
        }

        #endregion Properties

        #region Methods

        public override System.Linq.Expressions.Expression Convert(ComparisionExpression expression)
        {
            switch (expression.Type)
            {
                case ComparisionType.Equal:
                    {
                        return System.Linq.Expressions.Expression.Equal(
                            expression.Left.Convert(this),
                            expression.Right.Convert(this));
                    }
                case ComparisionType.Like:
                    {
                        return GetLikeExpression(expression);
                    }
                case ComparisionType.NotLike:
                    return System.Linq.Expressions.Expression.Not(GetLikeExpression(expression));
                case ComparisionType.NotEqual:
                    {
                        return System.Linq.Expressions.Expression.NotEqual(
                            expression.Left.Convert(this),
                            expression.Right.Convert(this));
                    }
                case ComparisionType.Less:
                    {
                        return System.Linq.Expressions.Expression.LessThan(
                            expression.Left.Convert(this),
                            expression.Right.Convert(this));
                    }
                case ComparisionType.LessOrEqual:
                    {
                        return System.Linq.Expressions.Expression.LessThanOrEqual(
                            expression.Left.Convert(this),
                            expression.Right.Convert(this));
                    }
                case ComparisionType.Greater:
                    {
                        return System.Linq.Expressions.Expression.GreaterThan(
                            expression.Left.Convert(this),
                            expression.Right.Convert(this));
                    }
                case ComparisionType.GreaterOrEqual:
                    {
                        return System.Linq.Expressions.Expression.GreaterThanOrEqual(
                            expression.Left.Convert(this),
                            expression.Right.Convert(this));
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private System.Linq.Expressions.Expression GetLikeExpression(ComparisionExpression expression)
        {
            var rightValue = expression.Right;//.Value == null ? null : expression.Right.Value.ToString();
            var startsWith = false;
            var endsWith = false;

            var leftCoalesce = System.Linq.Expressions.Expression.Coalesce(
                expression.Left.Convert(this),
                System.Linq.Expressions.Expression.Field(null, typeof(string).GetField("Empty")));
            var leftToString = System.Linq.Expressions.Expression.Call(
                leftCoalesce,
                typeof(string).GetMethod("ToString", new Type[0]));
            var leftToLower = System.Linq.Expressions.Expression.Call(
                leftToString,
                typeof(string).GetMethod("ToLower", new Type[0]));


            if (rightValue != null && rightValue.Value.Count(c => c == '*') == 1 && !rightValue.Value.StartsWith("*") && !rightValue.Value.EndsWith("*"))
            {
                var prefix = rightValue.Value.Substring(0, rightValue.Value.IndexOf('*'));
                var postfix = rightValue.Value.Substring(rightValue.Value.LastIndexOf('*') + 1);
                var prefixToLower = System.Linq.Expressions.Expression.Call(
                    new RightValueExpression() { Parent = expression, TypeName = expression.Right.TypeName, Value = prefix }.Convert(this),
                    typeof(string).GetMethod("ToLower", new Type[0]));
                var postfixToLower = System.Linq.Expressions.Expression.Call(
                    new RightValueExpression() { Parent = expression, TypeName = expression.Right.TypeName, Value = postfix }.Convert(this),
                    typeof(string).GetMethod("ToLower", new Type[0]));

                return System.Linq.Expressions.Expression.AndAlso(
                    System.Linq.Expressions.Expression.Call(
                        leftToLower,
                        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }),
                        prefixToLower),
                    System.Linq.Expressions.Expression.Call(
                        leftToLower,
                        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }),
                        postfixToLower));

            }
            else
            {
                if (rightValue.Value != null && rightValue.Value.Contains('*'))
                    {
                        if (rightValue.Value.StartsWith("*"))
                        {
                            endsWith = true;
                            rightValue = new RightValueExpression() { Parent = expression, TypeName = expression.Right.TypeName, Value = rightValue.Value.Substring(1) };
                        }
                        if (rightValue.Value.EndsWith("*"))
                        {
                            startsWith = true;
                            rightValue = new RightValueExpression() { Parent = expression, TypeName = expression.Right.TypeName, Value = rightValue.Value.Substring(0, rightValue.Value.Length - 1) };
                        }
                    }
                    else
                    {
                        startsWith = false;
                    }

                var rightToLower = System.Linq.Expressions.Expression.Call(
                    rightValue.Convert(this),
                    typeof(string).GetMethod("ToLower", new Type[0]));

                if (!startsWith && !endsWith)
                {
                    return System.Linq.Expressions.Expression.Call(
                        leftToLower,
                        typeof(string).GetMethod("Equals", new Type[] { typeof(string) }),
                        rightToLower);
                }
                if (startsWith && endsWith)
                {
                    return System.Linq.Expressions.Expression.Call(
                        leftToLower,
                        typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                        rightToLower);
                }
                if (startsWith)
                {
                    return System.Linq.Expressions.Expression.Call(
                        leftToLower,
                        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }),
                        rightToLower);
                }
                else
                {
                    return System.Linq.Expressions.Expression.Call(
                        leftToLower,
                        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }),
                        rightToLower);
                }
            }
        }

        public override System.Linq.Expressions.Expression Convert(LeftValueExpression expression)
        {
            return System.Linq.Expressions.Expression.MakeMemberAccess(_parameter, _parameter.Type.GetProperty(expression.Value));
        }

        public override System.Linq.Expressions.Expression Convert(RightValueExpression expression)
        {
            var type = Type.GetType(expression.TypeName ?? string.Empty);
            if (type == null)
            {
                type = typeof(string);
            }
            if (type == typeof(string))
            {
                return System.Linq.Expressions.Expression.Constant(expression.Value, type);
            }
            else if (type == typeof(bool))
            {
                return System.Linq.Expressions.Expression.Constant(System.Convert.ToBoolean(expression.Value), type);
            }
            else if (type == typeof(object) && expression.Value == null)
            {
                return System.Linq.Expressions.Expression.Constant(expression.Value, type);
            }
            else if (type.IsEnum)
            {
                return System.Linq.Expressions.Expression.Constant(Enum.Parse(type, expression.Value), type);
            }
            else if (Nullable.GetUnderlyingType(type) != null && Nullable.GetUnderlyingType(type).IsEnum)
            {
                if (expression.Value == null)
                    return System.Linq.Expressions.Expression.Constant(null, type);
                else
                    return System.Linq.Expressions.Expression.Constant(Enum.Parse(Nullable.GetUnderlyingType(type), expression.Value), type);
            }
            else
            {
                if (type == typeof(byte?))
                {
                    return System.Linq.Expressions.Expression.Constant((byte?)System.Convert.ToByte(expression.Value), type);
                }
                if (type == typeof(sbyte?))
                {
                    return System.Linq.Expressions.Expression.Constant((sbyte?)System.Convert.ToSByte(expression.Value), type);
                }
                if (type == typeof(char?))
                {
                    return System.Linq.Expressions.Expression.Constant((char?)System.Convert.ToChar(expression.Value), type);
                }
                if (type == typeof(bool?))
                {
                    return System.Linq.Expressions.Expression.Constant((bool?)System.Convert.ToBoolean(expression.Value), type);
                }
                if (type == typeof(float?))
                {
                    return System.Linq.Expressions.Expression.Constant((float?)System.Convert.ToSingle(expression.Value), type);
                }
                if (type == typeof(double?))
                {
                    return System.Linq.Expressions.Expression.Constant((double?)System.Convert.ToDouble(expression.Value), type);
                }
                if (type == typeof(short?))
                {
                    return System.Linq.Expressions.Expression.Constant((short?)System.Convert.ToInt16(expression.Value), type);
                }
                if (type == typeof(int))
                {
                    return System.Linq.Expressions.Expression.Constant(System.Convert.ToInt32(expression.Value), type);
                }
                if (type == typeof(int?))
                {
                    return System.Linq.Expressions.Expression.Constant((int?)System.Convert.ToInt32(expression.Value), type);
                }
                if (type == typeof(long?))
                {
                    return System.Linq.Expressions.Expression.Constant((long?)System.Convert.ToInt64(expression.Value), type);
                }
                if (type == typeof(decimal?))
                {
                    return System.Linq.Expressions.Expression.Constant((decimal?)System.Convert.ToInt32(expression.Value), type);
                }
                if (type == typeof(DateTime?))
                {
                    return System.Linq.Expressions.Expression.Constant((DateTime?)System.Convert.ToDateTime(expression.Value), type);
                }
            }
            throw new NotSupportedException();
        }

        public override System.Linq.Expressions.Expression Convert(LogicalExpression expression)
        {
            if (expression.Operands == null || expression.Operands.Count == 0)
            {
                return System.Linq.Expressions.Expression.Constant(true, typeof(bool));
            }
            System.Linq.Expressions.Expression exp = null;
            foreach (var item in expression.Operands)
            {
                if (exp == null)
                {
                    exp = item.Convert<System.Linq.Expressions.Expression>(this);
                }
                else
                {
                    switch (expression.Type)
                    {
                        case LogicalOperatorType.And:
                            {
                                exp = System.Linq.Expressions.Expression.AndAlso(
                                    exp,
                                    item.Convert(this));
                            }
                            break;
                        case LogicalOperatorType.Or:
                            {
                                exp = System.Linq.Expressions.Expression.OrElse(
                                    exp,
                                    item.Convert(this));
                            }
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            return exp;
        }

        #endregion Methods
    }
}
