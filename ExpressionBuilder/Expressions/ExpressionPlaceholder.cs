using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ExpressionBuilder.BaseTypes;

namespace ExpressionBuilder.Expressions
{
    public partial class ExpressionPlaceholder : Expression
    {
        #region .ctors

        public ExpressionPlaceholder()
        {
        }

        #endregion .ctors

        #region Properties

        /// <summary>
        /// Gets/sets ReplaceCommand.
        /// </summary>
        public DelegateCommand<Expression> ReplaceCommand
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return p_ReplaceCommand; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                if (p_ReplaceCommand != value)
                {
                    p_ReplaceCommand = value;
                    OnPropertyChanged("ReplaceCommand");
                    OnReplaceCommandChanged();
                }
            }
        }
        private DelegateCommand<Expression> p_ReplaceCommand;
        partial void OnReplaceCommandChanged();

        #endregion Properties
    }
}
