using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ExpressionBuilder.BaseTypes
{
    public class DelegateCommand<T> : ICommand
        where T : class
    {
        #region .ctors

        public DelegateCommand(Action<T> action)
        {
            _action = action;
        }

        #endregion .ctors

        #region Fields

        private Action<T> _action;

        #endregion Fields

        #region Methods

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action(parameter as T);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        #endregion Methods
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        #region .ctors

        public DelegateCommand(Action action)
            : base((o) => action())
        {
        }

        #endregion .ctors
    }

    public class DelegateCommand<T1, T2> : ICommand
    {
        #region .ctors

        public DelegateCommand(Action<T1, T2> action)
        {
            _action = action;
        }

        #endregion .ctors

        #region Fields

        private Action<T1, T2> _action;

        #endregion Fields

        #region Methods

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(T1 parameter1, T2 parameter2)
        {
            if (_action != null)
            {
                _action(parameter1, parameter2);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        #endregion Methods
    }
}
