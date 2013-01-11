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

namespace ExpressionBuilder.Controls
{
    public class ComparisionExpressionViewer : Control
    {
        #region .ctors

        static ComparisionExpressionViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComparisionExpressionViewer), new FrameworkPropertyMetadata(typeof(ComparisionExpressionViewer)));
        }

        public ComparisionExpressionViewer()
        {
            RemoveCommand = new DelegateCommand(Remove);
            ChangeToEqCommand = new DelegateCommand(() => Expression.Type = ComparisionType.Equal);
            ChangeToNotEqCommand = new DelegateCommand(() => Expression.Type = ComparisionType.NotEqual);
            ChangeToLikeCommand = new DelegateCommand(() => Expression.Type = ComparisionType.Like);
            ChangeToNotLikeCommand = new DelegateCommand(() => Expression.Type = ComparisionType.NotLike);
            ChangeToGreaterCommand = new DelegateCommand(() => Expression.Type = ComparisionType.Greater);
            ChangeToGreaterOrEqCommand = new DelegateCommand(() => Expression.Type = ComparisionType.GreaterOrEqual);
            ChangeToLessCommand = new DelegateCommand(() => Expression.Type = ComparisionType.Less);
            ChangeToLessOrEqCommand = new DelegateCommand(() => Expression.Type = ComparisionType.LessOrEqual);
            WrapWithAndCommand = new DelegateCommand(WrapWithAnd);
            WrapWithOrCommand = new DelegateCommand(WrapWithOr);
        }

        #endregion .ctors

        #region Properties

        #region Expression

        public ComparisionExpression Expression
        {
            get { return (ComparisionExpression)GetValue(ExpressionProperty); }
            set { SetValue(ExpressionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Expression.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpressionProperty =
            DependencyProperty.Register("Expression", typeof(ComparisionExpression), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null, (s, e) =>
                {
                    ((ComparisionExpressionViewer)s).OnExpressionChanged(e);
                }));

        private void OnExpressionChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                ((ComparisionExpression)e.OldValue).Left.ValueChanged -= Left_ValueChanged;
            }
            if (e.NewValue != null)
            {
                ((ComparisionExpression)e.NewValue).Left.ValueChanged += Left_ValueChanged;
            }
        }

        private void Left_ValueChanged(object sender, EventArgs e)
        {
            ReapplyTemplates(this);
        }

        #endregion Expression

        #region RemoveCommand

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemoveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion RemoveCommand

        #region ChangeToEqCommand

        public ICommand ChangeToEqCommand
        {
            get { return (ICommand)GetValue(ChangeToEqCommandProperty); }
            set { SetValue(ChangeToEqCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToEqCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToEqCommandProperty =
            DependencyProperty.Register("ChangeToEqCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToEqCommand

        #region ChangeToNotEqCommand

        public ICommand ChangeToNotEqCommand
        {
            get { return (ICommand)GetValue(ChangeToNotEqCommandProperty); }
            set { SetValue(ChangeToNotEqCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToNotEqCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToNotEqCommandProperty =
            DependencyProperty.Register("ChangeToNotEqCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToNotEqCommand

        #region ChangeToLikeCommand

        public ICommand ChangeToLikeCommand
        {
            get { return (ICommand)GetValue(ChangeToLikeCommandProperty); }
            set { SetValue(ChangeToLikeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToLikeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToLikeCommandProperty =
            DependencyProperty.Register("ChangeToLikeCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToLikeCommand

        #region ChangeToNotLikeCommand

        public ICommand ChangeToNotLikeCommand
        {
            get { return (ICommand)GetValue(ChangeToNotLikeCommandProperty); }
            set { SetValue(ChangeToNotLikeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToNotLikeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToNotLikeCommandProperty =
            DependencyProperty.Register("ChangeToNotLikeCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToNotLikeCommand

        #region ChangeToGreaterCommand

        public ICommand ChangeToGreaterCommand
        {
            get { return (ICommand)GetValue(ChangeToGreaterCommandProperty); }
            set { SetValue(ChangeToGreaterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToGreaterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToGreaterCommandProperty =
            DependencyProperty.Register("ChangeToGreaterCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToGreaterCommand

        #region ChangeToGreaterOrEqCommand

        public ICommand ChangeToGreaterOrEqCommand
        {
            get { return (ICommand)GetValue(ChangeToGreaterOrEqCommandProperty); }
            set { SetValue(ChangeToGreaterOrEqCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToGreaterOrEqCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToGreaterOrEqCommandProperty =
            DependencyProperty.Register("ChangeToGreaterOrEqCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToGreaterOrEqCommand

        #region ChangeToLessCommand

        public ICommand ChangeToLessCommand
        {
            get { return (ICommand)GetValue(ChangeToLessCommandProperty); }
            set { SetValue(ChangeToLessCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToLessCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToLessCommandProperty =
            DependencyProperty.Register("ChangeToLessCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToLessCommand

        #region ChangeToLessOrEqCommand

        public ICommand ChangeToLessOrEqCommand
        {
            get { return (ICommand)GetValue(ChangeToLessOrEqCommandProperty); }
            set { SetValue(ChangeToLessOrEqCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeToLessOrEqCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeToLessOrEqCommandProperty =
            DependencyProperty.Register("ChangeToLessOrEqCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion ChangeToLessOrEqCommand

        #region WrapWithAndCommand

        public ICommand WrapWithAndCommand
        {
            get { return (ICommand)GetValue(WrapWithAndCommandProperty); }
            set { SetValue(WrapWithAndCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WrapWithAndCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WrapWithAndCommandProperty =
            DependencyProperty.Register("WrapWithAndCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion WrapWithAndCommand

        #region WrapWithOrCommand

        public ICommand WrapWithOrCommand
        {
            get { return (ICommand)GetValue(WrapWithOrCommandProperty); }
            set { SetValue(WrapWithOrCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WrapWithOrCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WrapWithOrCommandProperty =
            DependencyProperty.Register("WrapWithOrCommand", typeof(ICommand), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(null));

        #endregion WrapWithOrCommand

		#region IsHighlighted

		public bool IsHighlighted
		{
			get { return (bool)GetValue(IsHighlightedProperty); }
			set { SetValue(IsHighlightedProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsHighlighted.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsHighlightedProperty =
			DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(ComparisionExpressionViewer), new UIPropertyMetadata(false));

		#endregion IsHighlighted

        #endregion Properties

        #region Methods

        private void Remove()
        {
            var scope = this.LookUp(d => ExpressionViewer.GetRemoveExpressionHandler(d) != null);
            if (scope != null)
            {
                ExpressionViewer.GetRemoveExpressionHandler(scope).Execute(Expression);
            }
        }

        private void WrapWithAnd()
        {
            var scope = this.LookUp(d => ExpressionViewer.GetReplaceExpressionHandler(d) != null);
            if (scope != null)
            {
                ExpressionViewer.GetReplaceExpressionHandler(scope).Execute(Expression, LogicalExpression.And(Expression));
            }
        }

        private void WrapWithOr()
        {
            var scope = this.LookUp(d => ExpressionViewer.GetReplaceExpressionHandler(d) != null);
            if (scope != null)
            {
                ExpressionViewer.GetReplaceExpressionHandler(scope).Execute(Expression, LogicalExpression.Or(Expression));
            }
        }

        private void ReapplyTemplates(DependencyObject obj)
        {
            if (obj is FrameworkElement && (obj as FrameworkElement).Name == "CC")
            {
                if ((obj as FrameworkElement).DataContext == Expression.Right)
                {
                    var cc = obj as ContentControl;
                    if (cc != null)
                    {
                        cc.DataContext = null;
                        cc.DataContext = Expression.Right;
                    }
                }
            }
            else
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    ReapplyTemplates(VisualTreeHelper.GetChild(obj, i));
                }
            }
        }

        #endregion Methods
    }
}
