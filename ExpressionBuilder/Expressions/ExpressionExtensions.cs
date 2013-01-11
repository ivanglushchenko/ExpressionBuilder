using ExpressionBuilder.Expressions.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExpressionBuilder.Expressions
{
    public static class ExpressionExtensions
    {
        #region Methods

        public static Func<T, bool> ToPredicate<T>(this Expression expression)
        {
            var converter = new LinqExpressionConverter(typeof(T));
            var body = converter.Convert(expression);
            var exp = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, converter.Parameter);
            return exp.Compile();
        }

        public static string GetPropertyName<In, Out>(System.Linq.Expressions.Expression<Func<In, Out>> getter)
        {
            Debug.Assert(getter != null);
            Debug.Assert(getter.NodeType == System.Linq.Expressions.ExpressionType.Lambda);
            Debug.Assert(getter.Body.NodeType == System.Linq.Expressions.ExpressionType.MemberAccess || getter.Body.NodeType == System.Linq.Expressions.ExpressionType.Convert);

            var mAcc = getter.Body as System.Linq.Expressions.MemberExpression;
            if (mAcc == null)
            {
                if (getter.Body is System.Linq.Expressions.UnaryExpression)
                {
                    mAcc = (getter.Body as System.Linq.Expressions.UnaryExpression).Operand as System.Linq.Expressions.MemberExpression;
                }
            }
            Debug.Assert(mAcc != null);
            return mAcc.Member.Name;
        }

        public static Expression ToExpression<In, Out>(System.Linq.Expressions.Expression getter)
        {
            Debug.Assert(getter != null);
            if (getter is System.Linq.Expressions.LambdaExpression)
                getter = ((System.Linq.Expressions.LambdaExpression)getter).Body;

            if (getter is System.Linq.Expressions.BinaryExpression)
            {
                var bExp = (System.Linq.Expressions.BinaryExpression)getter;

                if (getter.NodeType == System.Linq.Expressions.ExpressionType.AndAlso)
                {
                    return new LogicalExpression(ToExpression<In, Out>(bExp.Left), ToExpression<In, Out>(bExp.Right)) { Type = LogicalOperatorType.And };
                }
                else if (getter.NodeType == System.Linq.Expressions.ExpressionType.OrElse)
                {
                    return new LogicalExpression(ToExpression<In, Out>(bExp.Left), ToExpression<In, Out>(bExp.Right)) { Type = LogicalOperatorType.Or };
                }
                else
                {
                    var lOp = getter.NodeType == System.Linq.Expressions.ExpressionType.NotEqual ? ComparisionType.NotEqual : ComparisionType.Equal;
                    var left = Convert<In>(bExp.Left, null);
                    var right = Convert<In>(bExp.Right, null);
                    if (right.Value == null && right.TypeName == typeof(object).FullName)
                    {
                        // this is null exp which should have the same type as the left part
                        right.TypeName = left.TypeName;
                    }
                    return new ComparisionExpression()
                    {
                        Type = lOp,
                        Left = (LeftValueExpression)left,
                        Right = (RightValueExpression)right
                    };
                }
            }
            else
            {
                return new ComparisionExpression()
                {
                    Type = ComparisionType.Equal, 
                    Left = (LeftValueExpression)Convert<In>(getter, false), 
                    Right = (RightValueExpression)Convert<Out>(getter, true)
                };
            }
        }

        private static ValueExpression Convert<In>(System.Linq.Expressions.Expression exp, bool? isValue)
        {
            if (exp.NodeType == System.Linq.Expressions.ExpressionType.MemberAccess)
            {
                var mExp = (System.Linq.Expressions.MemberExpression)exp;
                if (mExp.Member.MemberType == MemberTypes.Property)
                {
                    if (isValue == null)
                    {
                        if (mExp.Expression.Type == typeof(In))
                        {
                            return new LeftValueExpression()
                            {
                                Value = mExp.Member.Name,
                                TypeName = ((PropertyInfo)mExp.Member).PropertyType.FullName
                            };
                        }
                        else
                        {
                            var value = System.Linq.Expressions.Expression.Lambda(mExp).Compile().DynamicInvoke();
                            return new RightValueExpression()
                            {
                                Value = (value ?? string.Empty).ToString(),
                                TypeName = ((PropertyInfo)mExp.Member).PropertyType.FullName
                            };
                        }
                    }
                    else
                    {
                        if (isValue == false)
                        {
                            return new LeftValueExpression()
                            {
                                Value = mExp.Member.Name,
                                TypeName = ((PropertyInfo)mExp.Member).PropertyType.FullName
                            };
                        }
                        else
                        {
                            var value = System.Linq.Expressions.Expression.Lambda(mExp).Compile().DynamicInvoke();
                            return new RightValueExpression()
                            {
                                Value = (value ?? string.Empty).ToString(),
                                TypeName = ((PropertyInfo)mExp.Member).PropertyType.FullName
                            };
                        }
                    }
                }
                else if (mExp.Member.MemberType == MemberTypes.Field)
                {
                    var value = System.Linq.Expressions.Expression.Lambda(mExp).Compile().DynamicInvoke();
                    return new RightValueExpression()
                    {
                        Value = (value ?? string.Empty).ToString(),
                        TypeName = ((FieldInfo)mExp.Member).FieldType.FullName
                    };
                }
                else
                {
                    return new LeftValueExpression()
                    {
                        Value = mExp.Member.Name
                    };
                }
            }
            if (exp.NodeType == System.Linq.Expressions.ExpressionType.Constant)
            {
                var cExp = (System.Linq.Expressions.ConstantExpression)exp;
                return RightValueExpression.Create(cExp.Value);
            }
            if (exp.NodeType == System.Linq.Expressions.ExpressionType.Call)
            {
                var mExp = (System.Linq.Expressions.MethodCallExpression)exp;
                var value = System.Linq.Expressions.Expression.Lambda(mExp).Compile().DynamicInvoke();
                return RightValueExpression.Create(value);
            }
            if (exp.NodeType == System.Linq.Expressions.ExpressionType.Convert)
            {
                var uExp = (System.Linq.Expressions.UnaryExpression)exp;
                return (ValueExpression)Convert<In>(uExp.Operand, null);
            }
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
