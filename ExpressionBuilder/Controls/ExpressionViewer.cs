using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpressionBuilder.Expressions;
using ExpressionBuilder.BaseTypes;
using System.Diagnostics;

namespace ExpressionBuilder.Controls
{
    public class ExpressionViewer : Control
    {
        #region .ctors

        static ExpressionViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpressionViewer), new FrameworkPropertyMetadata(typeof(ExpressionViewer)));
        }

        public ExpressionViewer()
        {
            RefreshExtendedExpression();
            SetRemoveExpressionHandler(this, new DelegateCommand<ExpressionBuilder.Expressions.Expression>(RemoveExpression));
            SetReplaceExpressionHandler(this, new DelegateCommand<Expressions.Expression, Expressions.Expression>(ReplaceExpression));
        }

        #endregion .ctors

		#region Fields

		private DependencyObject _lastHighlightedItem;

		#endregion Fields

        #region Properties

        #region Expression

        public ExpressionBuilder.Expressions.Expression Expression
        {
            get { return (ExpressionBuilder.Expressions.Expression)GetValue(ExpressionProperty); }
            set { SetValue(ExpressionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Expression.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpressionProperty =
            DependencyProperty.Register("Expression", typeof(ExpressionBuilder.Expressions.Expression), typeof(ExpressionViewer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                ((ExpressionViewer)s).RefreshExtendedExpression();
            }));

        #endregion Expression

        #region ExtendedExpression

        public ExpressionBuilder.Expressions.Expression ExtendedExpression
        {
            get { return (ExpressionBuilder.Expressions.Expression)GetValue(ExtendedExpressionProperty); }
            set { SetValue(ExtendedExpressionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExtendedExpression.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtendedExpressionProperty =
            DependencyProperty.Register("ExtendedExpression", typeof(ExpressionBuilder.Expressions.Expression), typeof(ExpressionViewer), new UIPropertyMetadata(null));

        #endregion ExtendedExpression

        #region RemoveExpressionHandler

        public static DelegateCommand<ExpressionBuilder.Expressions.Expression> GetRemoveExpressionHandler(DependencyObject obj)
        {
            return (DelegateCommand<ExpressionBuilder.Expressions.Expression>)obj.GetValue(RemoveExpressionHandlerProperty);
        }

        public static void SetRemoveExpressionHandler(DependencyObject obj, DelegateCommand<ExpressionBuilder.Expressions.Expression> value)
        {
            obj.SetValue(RemoveExpressionHandlerProperty, value);
        }

        // Using a DependencyProperty as the backing store for RemoveExpressionHandler.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoveExpressionHandlerProperty =
            DependencyProperty.RegisterAttached("RemoveExpressionHandler", typeof(DelegateCommand<ExpressionBuilder.Expressions.Expression>), typeof(ExpressionViewer), new UIPropertyMetadata(null));

        #endregion RemoveExpressionHandler

        #region ReplaceExpressionHandler

        public static DelegateCommand<ExpressionBuilder.Expressions.Expression, ExpressionBuilder.Expressions.Expression> GetReplaceExpressionHandler(DependencyObject obj)
        {
            return (DelegateCommand<ExpressionBuilder.Expressions.Expression, ExpressionBuilder.Expressions.Expression>)obj.GetValue(ReplaceExpressionHandlerProperty);
        }

        public static void SetReplaceExpressionHandler(DependencyObject obj, DelegateCommand<ExpressionBuilder.Expressions.Expression, ExpressionBuilder.Expressions.Expression> value)
        {
            obj.SetValue(ReplaceExpressionHandlerProperty, value);
        }

        // Using a DependencyProperty as the backing store for ReplaceExpressionHandler.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReplaceExpressionHandlerProperty =
            DependencyProperty.RegisterAttached("ReplaceExpressionHandler", typeof(DelegateCommand<ExpressionBuilder.Expressions.Expression, ExpressionBuilder.Expressions.Expression>), typeof(ExpressionViewer), new UIPropertyMetadata(null));

        #endregion ReplaceExpressionHandler

        #region LeftValueTemplateSelector

        public DataTemplateSelector LeftValueTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LeftValueTemplateSelectorProperty); }
            set { SetValue(LeftValueTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftValueTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftValueTemplateSelectorProperty =
            DependencyProperty.Register("LeftValueTemplateSelector", typeof(DataTemplateSelector), typeof(ExpressionViewer), new UIPropertyMetadata(null));

        #endregion LeftValueTemplateSelector

        #region RightValueTemplateSelector

        public DataTemplateSelector RightValueTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(RightValueTemplateSelectorProperty); }
            set { SetValue(RightValueTemplateSelectorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightValueTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightValueTemplateSelectorProperty =
            DependencyProperty.Register("RightValueTemplateSelector", typeof(DataTemplateSelector), typeof(ExpressionViewer), new UIPropertyMetadata(null));

        #endregion RightValueTemplateSelector

        #endregion Properties

        #region Methods

        private void RefreshExtendedExpression()
        {
            if (Expression != null)
            {
                ExtendedExpression = Expression;
            }
            else
            {
                ExtendedExpression = new ExpressionPlaceholder() { ReplaceCommand = new DelegateCommand<ExpressionBuilder.Expressions.Expression>(ReplacePlaceholder) };
            }
        }

        private void ReplacePlaceholder(ExpressionBuilder.Expressions.Expression exp)
        {
            Expression = exp;
            Expression.Validate();
        }

        private void RemoveExpression(ExpressionBuilder.Expressions.Expression exp)
        {
            Debug.Assert(exp == Expression);

            Expression = null;
        }

        private void ReplaceExpression(ExpressionBuilder.Expressions.Expression oldExp, ExpressionBuilder.Expressions.Expression newExp)
        {
            Debug.Assert(oldExp == Expression);

            Expression = newExp;
            Expression.Validate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var d = InputHitTest(e.GetPosition(this)) as DependencyObject;
			var i = (DependencyObject)d.GetParent<ComparisionExpressionViewer>() ?? d.GetParent<LogicalExpressionViewer>();

			if (_lastHighlightedItem != null && (i != _lastHighlightedItem))
			{
				if (_lastHighlightedItem is ComparisionExpressionViewer)
					(_lastHighlightedItem as ComparisionExpressionViewer).IsHighlighted = false;
				else
					(_lastHighlightedItem as LogicalExpressionViewer).IsHighlighted = false;
			}

            if (i != null)
            {
				if (i is ComparisionExpressionViewer)
				{
					(i as ComparisionExpressionViewer).IsHighlighted = true;
					_lastHighlightedItem = i;
				}
				else if (i is LogicalExpressionViewer)
				{
					(i as LogicalExpressionViewer).IsHighlighted = true;
					_lastHighlightedItem = i;
				}
            }
        }

        #endregion Methods
    }
}
