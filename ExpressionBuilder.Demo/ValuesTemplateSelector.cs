using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ExpressionBuilder.Expressions;
using ExpressionBuilder.BaseTypes;
using System.Windows;
using ExpressionBuilder.Controls;
using System.Diagnostics;

namespace ExpressionBuilder.Demo
{
    public class ValuesTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var viewer = container.GetParent<ExpressionViewer>();
            var rvExp = item as RightValueExpression;

            Debug.Assert(viewer != null);
            Debug.Assert(rvExp != null);

            if (((ComparisionExpression)rvExp.Parent).Left.Value != null)
            {
                if (((ComparisionExpression)rvExp.Parent).Left.Value.ToString() == "Gender")
                {
                    return (DataTemplate)viewer.Resources["DT_ComboBox"];
                }
                return (DataTemplate)viewer.Resources["DT_Text"];
            }
            return (DataTemplate)viewer.Resources["DT_Empty"];
        }
    }
}
