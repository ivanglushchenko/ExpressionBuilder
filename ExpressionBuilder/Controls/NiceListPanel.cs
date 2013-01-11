using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections;
using System.Windows.Shapes;

namespace ExpressionBuilder.Controls
{
    public partial class NiceListPanel : Panel
    {
        #region .ctors

        public NiceListPanel()
        {
            SnapsToDevicePixels = true;
            UseLayoutRounding = true;
        }

        #endregion .ctors

        #region Fields

        private Path _sharedLine;
        private List<Path> _lines = new List<Path>();

        #endregion Fields

        #region Methods

        protected override Int32 VisualChildrenCount
        {
            get 
            {
                return 
                    _sharedLine == null
                    ? base.VisualChildrenCount + _lines.Count
                    : base.VisualChildrenCount + _lines.Count + 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (_sharedLine == null)
            {
                if (index < _lines.Count)
                {
                    return _lines[index];
                }
                return base.GetVisualChild(index - _lines.Count);
            }
            if (index == 0)
            {
                return _sharedLine;
            }
            if (index - 1 < _lines.Count)
            {
                return _lines[index - 1];
            }
            return base.GetVisualChild(index - _lines.Count - 1);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (_sharedLine == null)
            {
                _sharedLine = new Path() { Stroke = Brushes.Gray, StrokeThickness = 1, SnapsToDevicePixels = true };
                AddVisualChild(_sharedLine);
            }

            var uiElements = InternalChildren.OfType<UIElement>().ToList();
            if (uiElements.Count > _lines.Count)
            {
                foreach (var item in Enumerable.Range(0, uiElements.Count - _lines.Count))
                {
                    var b = new Path() { Stroke = Brushes.Gray, StrokeThickness = 1, SnapsToDevicePixels = true };
                    _lines.Add(b);
                    AddVisualChild(b);
                }
            }
            if (uiElements.Count < _lines.Count)
            {
                while (_lines.Count > uiElements.Count)
                {
                    RemoveVisualChild(_lines[0]);
                    _lines.RemoveAt(0);
                }
            }

            foreach (var item in uiElements)
            {
                item.Measure(constraint);
            }
            foreach (var item in _lines)
            {
                item.Measure(constraint);
            }

            _sharedLine.Measure(constraint);

            return new Size(uiElements.Sum(e => e.DesiredSize.Width), uiElements.Max(e => e.DesiredSize.Height) + 20);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var x = 0.0;
            var xFirstItem = double.NaN;
            var xLastItem = double.NaN;
            var uiElements = InternalChildren.OfType<UIElement>().ToList();
            for (int i = 0; i < uiElements.Count; i++)
            {
                var item = uiElements[i];
                item.Arrange(new Rect(new Point(x, 20), item.DesiredSize));
                if (double.IsNaN(xFirstItem))
                {
                    xFirstItem = item.DesiredSize.Width / 2.0;
                }
                xLastItem = x + item.DesiredSize.Width / 2.0;
                x += item.DesiredSize.Width;

                var line = _lines[i];
                line.Data = new PathGeometry(new PathFigure[] { new PathFigure(new Point(xLastItem, 10), new PathSegment[] { new LineSegment(new Point(xLastItem, 20), true) }, false) });
                line.Arrange(new Rect(new Point(0, 0), arrangeSize));
            }

            if (!double.IsNaN(xFirstItem))
            {
                _sharedLine.Data = new PathGeometry(new PathFigure[] { 
                    new PathFigure(new Point(xFirstItem, 10), new PathSegment[] { new LineSegment(new Point(xLastItem, 10), true) }, false),
                    new PathFigure(new Point(x / 2.0, 0), new PathSegment[] { new LineSegment(new Point(x / 2.0, 10), true) }, false)});
            }
            else
            {
                _sharedLine.Data = null;
            }

            _sharedLine.Arrange(new Rect(new Point(0, 0), arrangeSize));

            return arrangeSize;
        }

        #endregion Methods
    }
}
