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

namespace ExpressionBuilder.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

			DataContext = new MainWindowModel();
            //DataContext = new LogicalExpression(new ExpressionBuilder.Expressions.Expression[] { 
            //    new ComparisionExpression("left", "right"), 
            //    new ComparisionExpression() { Left = ValueExpression.From("l"), Right = ValueExpression.From("vsdksjdfhks") } })
            //{
            //    Type = LogicalOperatorType.And
            //};

        }
    }
}
