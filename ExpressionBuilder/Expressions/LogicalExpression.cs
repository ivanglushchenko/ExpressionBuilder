using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ExpressionBuilder.Expressions.Converters;

namespace ExpressionBuilder.Expressions
{
    public partial class LogicalExpression : Expression
    {
        #region .ctors

        public LogicalExpression(params Expression[] operands)
        {
            if (operands != null)
            {
                foreach (var item in operands)
                {
                    item.Parent = this;
                }
                Operands = new ObservableCollection<Expression>(operands.Where(t => t != null));
            }
            else
            {
                Operands = new ObservableCollection<Expression>();
            }
            Operands.CollectionChanged += Operands_CollectionChanged;
        }

        #endregion .ctors

        #region Properties

        /// <summary>
        /// Gets/sets Type.
        /// </summary>
        public LogicalOperatorType Type
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Type; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (p_Type != value)
                {
                    p_Type = value;
                    OnPropertyChanged("Type");
                    OnTypeChanged();
                }
            }
        }
        private LogicalOperatorType p_Type;
        partial void OnTypeChanged();

        /// <summary>
        /// Gets/sets Operands.
        /// </summary>
        public ObservableCollection<Expression> Operands
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Operands; }
            [System.Diagnostics.DebuggerStepThrough]
            private set
            {
                if (p_Operands != value)
                {
                    p_Operands = value;
                    OnPropertyChanged("Operands");
                    OnOperandsChanged();
                }
            }
        }
        private ObservableCollection<Expression> p_Operands;
        partial void OnOperandsChanged();

        #endregion Properties

        #region Methods

        public override void Foreach(Action<string, Expression> action, string name)
        {
            foreach (var item in Operands)
            {
                item.Foreach(action, string.Format("Operand {0}", Operands.IndexOf(item) + 1));
            }
            base.Foreach(action, name);
        }

        public static LogicalExpression And(Expression operand)
        {
            var lExp = new LogicalExpression(null) { Type = LogicalOperatorType.And, Operands = new ObservableCollection<Expression>() { operand } };
            operand.Parent = lExp;
            return lExp;
        }

        public static LogicalExpression Or(Expression operand)
        {
            var lExp = new LogicalExpression(null) { Type = LogicalOperatorType.Or, Operands = new ObservableCollection<Expression>() { operand } };
            operand.Parent = lExp;
            return lExp;
        }

        public override T Convert<T>(Converter<T> converter)
        {
            return converter.Convert(this);
        }

        private void Operands_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<Expression>())
                {
                    item.Parent = this;
                }
            }
        }

        #endregion Methods
    }
}
