using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

using resx = MyApp.Resources.Languages;

namespace MyApp.Converters
{
    public class NullToReceivedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return resx.AppResource.receive;
            else
                return resx.AppResource.cancelReceive;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
