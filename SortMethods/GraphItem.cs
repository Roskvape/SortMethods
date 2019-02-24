using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;

namespace SortMethods
{
    public class GraphItem
    {
        public Rectangle _bar { get; set; }
        public Label _itemIndex { get; set; }
        public Rectangle _indexBackground { get; set; }
        public Label _itemValue { get; set; }
        public Rectangle _valueBackground { get; set; }

        public GraphItem(Rectangle bar, Label itemIndex, Rectangle indexBackground, Label itemValue, Rectangle valueBackground)
        {
            _bar = bar;
            _itemIndex = itemIndex;
            _indexBackground = indexBackground;
            _itemValue = itemValue;
            _valueBackground = valueBackground;
        }

        public void SetAllColors(Color color)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            SolidColorBrush brush2 = new SolidColorBrush((Color)Application.Current.FindResource("SortedItemColor"));
            _bar.Fill = brush;
            _indexBackground.Fill = brush2;
            _valueBackground.Fill = brush;
        }

        public void UpdateBarHeight(int newHeight, int maxHeight, int totalItems)
        {
            _bar.Height = newHeight * (maxHeight / totalItems);
        }

        public void UpdateValue(int newValue)
        {
            _itemValue.Content = newValue;
        }
    }
}
