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
using System.Collections.ObjectModel;
using ExpressionBuilder.BaseTypes;
using System.Diagnostics;

namespace ExpressionBuilder.Controls
{
    public class LogicalExpressionViewer : Control
    {
        #region .ctors

        static LogicalExpressionViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LogicalExpressionViewer), new FrameworkPropertyMetadata(typeof(LogicalExpressionViewer)));
        }

        public LogicalExpressionViewer()
        {
            RemoveCommand = new DelegateCommand(Remove);
            ChangeToAndCommand = new DelegateCommand(() => Expression.Type = LogicalOperatorType.And);
            ChangeToOrCommand = new DelegateCommand(() => Expression.Type = LogicalOperatorType.Or);
            ExpressionViewer.SetRemoveExpressionHandler(this, new DelegateCommand<ExpressionBuilder.Expressions.Expression>(RemoveExpression));
            ExpressionViewer.SetReplaceExpressionHandler(this, new DelegateCommand<Expressions.Expression, Expressions.Expression>(ReplaceExpression));
        }

        #endregion .ctors

        #region Properties

        public LogicalExpression Expression
        {
            get { return (LogicalExpression)GetValue(ExpressionProperty); }
            set { SetValue(ExpressionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Expression.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpressionProperty =
            DependencyProperty.Register("Expression", typeof(LogicalExpression), typeof(LogicalExpressionViewer), new UIPropertyMetadata(null, (s, e) =>
                {
                    if (e.NewValue != null)
                    {
                        ((LogicalExpressionViewer)s).RefreshExtendedOperands();
                    }
                    else
                    {
                        ((LogicalExpressionViewer)s).ExtendedOperands = null;
                    }
                }));

        public ObservableCollection<ExpressionBuilder.Expressions.Expression> ExtendedOperands
        {
            get { return (ObservableCollection<ExpressionBuilder.Expressions.Expression>)GetValue(ExtendedOperandsProperty); }
            set { SetValue(ExtendedOperandsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExtendedOperands.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtendedOperandsProperty =
            DependencyProperty.Register("ExtendedOperands", typeof(ObservableCollection<ExpressionBuilder.Expressions.Expression>), typeof(LogicalExpressionViewer), new UIPropertyMetadata(null));

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemoveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(LogicalExpressionViewer), new UIPropertyMetadata(null));

        public ICommand ChangeToAndCommand
        {
            get { return (ICommand)GetValue(ChangeToAndCommandProperty); }
            set { SetValue(ChangeToAndCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToAndCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToAndCommandProperty =
            DependencyProperty.Register("ChangeToAndCommand", typeof(ICommand), typeof(LogicalExpressionViewer), new UIPropertyMetadata(null));

        public ICommand ChangeToOrCommand
        {
            get { return (ICommand)GetValue(ChangeToOrCommandProperty); }
            set { SetValue(ChangeToOrCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToOrCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToOrCommandProperty =
            DependencyProperty.Register("ChangeToOrCommand", typeof(ICommand), typeof(LogicalExpressionViewer), new UIPropertyMetadata(null));

		public bool IsHighlighted
		{
			get { return (bool)GetValue(IsHighlightedProperty); }
			set { SetValue(IsHighlightedProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsHighlighted.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsHighlightedProperty =
			DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(LogicalExpressionViewer), new UIPropertyMetadata(false));

        #endregion Properties

        #region Methods

        private void ReplacePlaceholder(ExpressionBuilder.Expressions.Expression exp)
        {
            Expression.Operands.Add(exp);
            exp.Parent = Expression;
            exp.Validate();
            RefreshExtendedOperands();
        }

        private void RefreshExtendedOperands()
        {
            var ops = new ObservableCollection<ExpressionBuilder.Expressions.Expression>(Expression.Operands);
            ops.Add(new ExpressionPlaceholder() { Parent = Expression, ReplaceCommand = new DelegateCommand<ExpressionBuilder.Expressions.Expression>(ReplacePlaceholder) });
            ExtendedOperands = ops;
        }

        private void Remove()
        {
            var scope = this.LookUp(d => ExpressionViewer.GetRemoveExpressionHandler(d) != null);
            if (scope != null)
            {
                ExpressionViewer.GetRemoveExpressionHandler(scope).Execute(Expression);
            }
        }

        private void RemoveExpression(ExpressionBuilder.Expressions.Expression exp)
        {
            Debug.Assert(Expression.Operands.Contains(exp));

            Expression.Operands.Remove(exp);
            RefreshExtendedOperands();
        }

        private void ReplaceExpression(ExpressionBuilder.Expressions.Expression oldExp, ExpressionBuilder.Expressions.Expression newExp)
        {
            Debug.Assert(Expression.Operands.Contains(oldExp));

            var index = Expression.Operands.IndexOf(oldExp);
            Expression.Operands.Remove(oldExp);
            Expression.Operands.Insert(index, newExp);
            newExp.Parent = Expression;
            newExp.Validate();
            RefreshExtendedOperands();
        }

        #endregion Methods
    }
}
