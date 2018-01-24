using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF2048.Assets
{
    public class ValueForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int val)
            {
                var b = Colors.SpielfeldBackgroundMap.FirstOrDefault(sbm => sbm.Key == val).Value.Color;
                return b.R + b.G + b.B > 3 * 127 ? Brushes.Black : Brushes.White;
            }

            return Brushes.Blue; //exception
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}