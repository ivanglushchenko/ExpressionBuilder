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

namespace ExpressionBuilder.Controls
{
    public class NiceComboBox : ComboBox
    {
        #region .ctors

        static NiceComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NiceComboBox), new FrameworkPropertyMetadata(typeof(NiceComboBox)));
        }

        public NiceComboBox()
        {
        }

        #endregion .ctors

        #region Properties

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NiceComboBox), new UIPropertyMetadata(null, (s, e) =>
                {
                }));

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(NiceComboBox), new UIPropertyMetadata(null));

        #endregion Properties

        #region Methods

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NiceComboBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NiceComboBoxItem;
        }

        #endregion Methods
    }
}
