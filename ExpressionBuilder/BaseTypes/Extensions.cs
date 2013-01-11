using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace ExpressionBuilder.BaseTypes
{
    public static class Extensions
    {
        #region Methods

        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }

        public static T GetParent<T>(this DependencyObject obj)
            where T : class
        {
            return LookUp(obj, d => d is T) as T;
        }

        public static DependencyObject LookUp(this DependencyObject obj, Predicate<DependencyObject> check)
        {
            var t = obj;
            while (t != null && !check(t = VisualTreeHelper.GetParent(t)));
            return t;
        }

        #endregion Methods
    }
}
