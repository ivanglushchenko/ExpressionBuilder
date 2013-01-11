using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ExpressionBuilder.Expressions
{
    public abstract partial class ValueExpression : Expression
    {
        #region .ctors

        //public ValueExpression(Expression parent)
        //    : base(parent)
        //{
        //    Debug.Assert(parent != null);
        //}

        #endregion .ctors

        #region Events

        public event EventHandler ValueChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets/sets Value.
        /// </summary>
        public string Value
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Value; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (p_Value != value)
                {
                    p_Value = value;
                    OnPropertyChanged("Value");
                    OnValueChanged();
                }
            }
        }
        private string p_Value;
        partial void OnValueChanged();

        public string TypeName
        {
            get { return _TypeName; }
            set
            {
                if (_TypeName != value)
                {
                    _TypeName = value;
                    OnPropertyChanged("TypeName");
                    OnTypeNameChanged();
                }
            }
        }
        private string _TypeName;
        partial void OnTypeNameChanged();

        #endregion Properties

        #region Methods

        partial void OnValueChanged()
        {
            Validate();

            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }

        protected override void ValidateCore()
        {
            Check("Value", () => !string.IsNullOrEmpty((Value ?? string.Empty).ToString()), "Value cannot be empty");
        }

        #endregion Methods
    }
}
