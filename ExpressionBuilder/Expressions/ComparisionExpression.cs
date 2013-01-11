using ExpressionBuilder.Expressions.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionBuilder.Expressions
{
    public partial class ComparisionExpression : Expression
    {
        #region .ctors

        public ComparisionExpression()
            : this(null, null)
        {
        }

        public ComparisionExpression(string left, string right)
        {
            Left = new LeftValueExpression() { Value = left };
            Right = new RightValueExpression() { Value = right };
        }

        #endregion .ctors

        #region Properties

        /// <summary>
        /// Gets/sets Type.
        /// </summary>
        public ComparisionType Type
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
        private ComparisionType p_Type;
        partial void OnTypeChanged();

        /// <summary>
        /// Gets/sets Left.
        /// </summary>
        public LeftValueExpression Left
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Left; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (p_Left != value)
                {
                    p_Left = value;
                    OnPropertyChanged("Left");
                    OnLeftChanged();
                }
            }
        }
        private LeftValueExpression p_Left;
        partial void OnLeftChanged();

        /// <summary>
        /// Gets/sets Right.
        /// </summary>
        public RightValueExpression Right
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Right; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (p_Right != value)
                {
                    p_Right = value;
                    OnPropertyChanged("Right");
                    OnRightChanged();
                }
            }
        }
        private RightValueExpression p_Right;
        partial void OnRightChanged();

        #endregion Properties

        #region Methods

        public override void Foreach(Action<string, Expression> action, string name)
        {
            Left.Foreach(action, "Left");
            Right.Foreach(action, "Right");
            base.Foreach(action, name);
        }

        //public override void ValidateCore()
        //{
        //    Left.ValidateCore();
        //    Check("Left", () => Left.Error == null, string.Format("Left: {0}", Left.Error));

        //    Right.ValidateCore();
        //    Check("Right", () => Right.Error == null, string.Format("Right: {0}", Right.Error));
        //}

        partial void OnLeftChanged()
        {
            if (Left != null)
                Left.Parent = this;
        }

        partial void OnRightChanged()
        {
            if (Right != null)
                Right.Parent = this;
        }

        public override T Convert<T>(Converter<T> converter)
        {
            return converter.Convert(this);
        }

        #endregion Methods
    }
}
