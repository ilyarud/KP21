using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;
using KP2021MathProcessor.ViewModel.Node;

namespace KP2021MathProcessor.ViewModel
{
    class NodeInfoConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is NodeContexInfo node && values[1] is Point point)
            {
                return new CreateNodeInfoViewModel(node, point);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is CreateNodeInfoViewModel createNodeInfoViewModel)
            {
                return new object[] { createNodeInfoViewModel.Info, createNodeInfoViewModel.Location };
            }
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
