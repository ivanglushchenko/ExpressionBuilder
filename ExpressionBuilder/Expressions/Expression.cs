using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ExpressionBuilder.BaseTypes;
using System.Diagnostics;
using ExpressionBuilder.Expressions.Converters;

namespace ExpressionBuilder.Expressions
{
    public abstract partial class Expression : ChangeAwareObject, IDataErrorInfo
    {
        #region .ctors

        public Expression()
        {
        }

        #endregion .ctors

        #region Properties

        /// <summary>
        /// Gets/sets Parent.
        /// </summary>
        public Expression Parent
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Parent; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (p_Parent != value)
                {
                    p_Parent = value;
                    OnPropertyChanged("Parent");
                    OnParentChanged();
                }
            }
        }
        private Expression p_Parent;
        partial void OnParentChanged();

		public Func<Expression, bool> CustomValidator
		{
			get
			{
				if (_CustomValidator != null)
					return _CustomValidator;
				if (Parent != null)
					return Parent.CustomValidator;
				return null;
			}
			set
			{
				if (_CustomValidator != value)
				{
					_CustomValidator = value;
					OnPropertyChanged("CustomValidator");
					OnCustomValidatorChanged();
				}
			}
		}
		private Func<Expression, bool> _CustomValidator;
		partial void OnCustomValidatorChanged();

        #endregion Properties

        #region Methods

        [DebuggerStepThrough]
        public virtual void Foreach(Action<string, Expression> action, string name)
        {
            if (action != null)
            {
                action(name, this);
            }
        }

        public void Validate()
        {
            Foreach((n, e) =>
                {
					if (CustomValidator == null || !CustomValidator(e))
						e.ValidateCore();
                },
                null);
            Func<Expression, Expression> getParent = null;
            getParent = e => e.Parent != null ? getParent(e.Parent) : e;
            var parent = getParent(this);
            parent.Foreach((n, e) =>
                {
                    if (n != null)
                    {
                        e.Parent.Check(n, () => e.Error == null, string.Format("{0}: {1}", n, e.Error));
                    }
                },
                null);
        }

        public virtual T Convert<T>(Converter<T> converter)
        {
            return converter.Convert(this);
        }

        #endregion Methods

        #region IDataErrorInfo Implementation

        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        public string Error
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_Error; }
            [System.Diagnostics.DebuggerStepThrough]
            private set
            {
                if (p_Error != value)
                {
                    p_Error = value;
                    OnPropertyChanged("Error");
                    OnErrorChanged();
                }
            }
        }
        private string p_Error;
        partial void OnErrorChanged();

        public string this[string columnName]
        {
            get { return _errors.ContainsKey(columnName ?? string.Empty) ? _errors[columnName ?? string.Empty] : null; }
        }

        protected virtual void ValidateCore()
        {
        }

        public void Check(string propertyName, Func<bool> check, string errorMessage)
        {
            if (check())
            {
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                }
            }
            else
            {
                _errors[propertyName] = errorMessage;
            }
            Error = _errors.Count == 0 ? null : string.Join(Environment.NewLine, _errors.Select(t => t.Value).ToArray());
        }

        #endregion IDataErrorInfo Implementation
    }
}
