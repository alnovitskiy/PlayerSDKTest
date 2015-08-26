using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ODS.SDK.Mobile.Shared.Controls;
using Xamarin.Forms;

namespace AlexOdsSdkTest.Helpers
{
    public class PlayerStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((HyperMediaPlayerState)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
