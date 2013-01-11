using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionBuilder.Expressions;

namespace ExpressionBuilder.Demo
{
	public class MainWindowModel
	{
		public MainWindowModel()
		{
			CustomValidation = (s) => false;

            Expression = new LogicalExpression(new ComparisionExpression("First Name", "Ivan"), new ComparisionExpression("Age", "18"));
		}

		public Expression Expression { get; set; }

		public Func<ExpressionBuilder.Expressions.Expression, bool> CustomValidation { get; set; }
	}
}
