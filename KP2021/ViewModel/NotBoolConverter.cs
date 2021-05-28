using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace KP2021MathProcessor.ViewModel
{
    class NotBoolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                return !val;
            }
            return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                return !val;
            }
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
