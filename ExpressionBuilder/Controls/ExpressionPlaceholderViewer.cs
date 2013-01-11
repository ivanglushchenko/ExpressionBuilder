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
    public class ExpressionPlaceholderViewer : Control
    {
        #region .ctors

        static ExpressionPlaceholderViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpressionPlaceholderViewer), new FrameworkPropertyMetadata(typeof(ExpressionPlaceholderViewer)));
        }

        public ExpressionPlaceholderViewer()
        {
            AddLogicalAndExpCommand = new DelegateCommand(() => AddLogicalExp(LogicalOperatorType.And));
            AddLogicalOrExpCommand = new DelegateCommand(() => AddLogicalExp(LogicalOperatorType.Or));
            AddComparisionEqExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.Equal));
            AddComparisionNotEqExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.NotEqual));
            AddComparisionLikeExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.Like));
            AddComparisionNotLikeExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.NotLike));
            AddComparisionGreaterExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.Greater));
            AddComparisionGreaterOrEqExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.GreaterOrEqual));
            AddComparisionLessExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.Less));
            AddComparisionLessOrEqExpCommand = new DelegateCommand(() => AddComparisionExp(ComparisionType.LessOrEqual));
        }

        #endregion .ctors

        #region Properties

        #region Placeholder

        public ExpressionPlaceholder Placeholder
        {
            get { return (ExpressionPlaceholder)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(ExpressionPlaceholder), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion Placeholder

        #region AddLogicalAndExpCommand

        public ICommand AddLogicalAndExpCommand
        {
            get { return (ICommand)GetValue(AddLogicalAndExpCommandProperty); }
            set { SetValue(AddLogicalAndExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddLogicalAndExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddLogicalAndExpCommandProperty =
            DependencyProperty.Register("AddLogicalAndExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddLogicalAndExpCommand

        #region AddLogicalOrExpCommand

        public ICommand AddLogicalOrExpCommand
        {
            get { return (ICommand)GetValue(AddLogicalOrExpCommandProperty); }
            set { SetValue(AddLogicalOrExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddLogicalOrExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddLogicalOrExpCommandProperty =
            DependencyProperty.Register("AddLogicalOrExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddLogicalOrExpCommand

        #region AddComparisionEqExpCommand

        public ICommand AddComparisionEqExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionEqExpCommandProperty); }
            set { SetValue(AddComparisionEqExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionEqExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionEqExpCommandProperty =
            DependencyProperty.Register("AddComparisionEqExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionEqExpCommand

        #region AddComparisionNotEqExpCommand

        public ICommand AddComparisionNotEqExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionNotEqExpCommandProperty); }
            set { SetValue(AddComparisionNotEqExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionNotEqExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionNotEqExpCommandProperty =
            DependencyProperty.Register("AddComparisionNotEqExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionNotEqExpCommand

        #region AddComparisionLikeExpCommand

        public ICommand AddComparisionLikeExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionLikeExpCommandProperty); }
            set { SetValue(AddComparisionLikeExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionLikeExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionLikeExpCommandProperty =
            DependencyProperty.Register("AddComparisionLikeExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionLikeExpCommand

        #region AddComparisionNotLikeExpCommand

        public ICommand AddComparisionNotLikeExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionNotLikeExpCommandProperty); }
            set { SetValue(AddComparisionNotLikeExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionNotLikeExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionNotLikeExpCommandProperty =
            DependencyProperty.Register("AddComparisionNotLikeExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionNotLikeExpCommand

        #region AddComparisionGreaterExpCommand

        public ICommand AddComparisionGreaterExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionGreaterExpCommandProperty); }
            set { SetValue(AddComparisionGreaterExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionGreaterExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionGreaterExpCommandProperty =
            DependencyProperty.Register("AddComparisionGreaterExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionGreaterExpCommand

        #region AddComparisionGreaterOrEqExpCommand

        public ICommand AddComparisionGreaterOrEqExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionGreaterOrEqExpCommandProperty); }
            set { SetValue(AddComparisionGreaterOrEqExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionGreaterOrEqExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionGreaterOrEqExpCommandProperty =
            DependencyProperty.Register("AddComparisionGreaterOrEqExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionGreaterOrEqExpCommand

        #region AddComparisionLessExpCommand

        public ICommand AddComparisionLessExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionLessExpCommandProperty); }
            set { SetValue(AddComparisionLessExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionLessExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionLessExpCommandProperty =
            DependencyProperty.Register("AddComparisionLessExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionLessExpCommand

        #region AddComparisionLessOrEqExpCommand

        public ICommand AddComparisionLessOrEqExpCommand
        {
            get { return (ICommand)GetValue(AddComparisionLessOrEqExpCommandProperty); }
            set { SetValue(AddComparisionLessOrEqExpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddComparisionLessOrEqExpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddComparisionLessOrEqExpCommandProperty =
            DependencyProperty.Register("AddComparisionLessOrEqExpCommand", typeof(ICommand), typeof(ExpressionPlaceholderViewer), new UIPropertyMetadata(null));

        #endregion AddComparisionLessOrEqExpCommand

        #endregion Properties

        #region Methods

        private void AddLogicalAndExp()
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new LogicalExpression(Placeholder.Parent) { Type = LogicalOperatorType.And });
            }
        }

        private void AddLogicalOrExp()
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new LogicalExpression(Placeholder.Parent) { Type = LogicalOperatorType.Or });
            }
        }

        private void AddComparisionEqExp()
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new ComparisionExpression() { Parent = Placeholder.Parent, Type = ComparisionType.Equal });
            }
        }

        private void AddComparisionNotEqExp()
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new ComparisionExpression() { Parent = Placeholder.Parent, Type = ComparisionType.NotEqual });
            }
        }

        private void AddComparisionLikeExp()
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new ComparisionExpression() { Parent = Placeholder.Parent, Type = ComparisionType.Like });
            }
        }

        private void AddLogicalExp(LogicalOperatorType type)
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new LogicalExpression() { Parent = Placeholder.Parent, Type = type });
            }
        }

        private void AddComparisionExp(ComparisionType type)
        {
            if (Placeholder != null && Placeholder.ReplaceCommand != null)
            {
                Placeholder.ReplaceCommand.Execute(new ComparisionExpression() { Parent = Placeholder.Parent, Type = type });
            }
        }

        #endregion Methods
    }
}
