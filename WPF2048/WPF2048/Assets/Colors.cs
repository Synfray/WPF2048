using System.Collections.Generic;
using System.Windows.Media;

namespace WPF2048.Assets
{
    public static class Colors
    {
        public static readonly Dictionary<int, SolidColorBrush> SpielfeldBackgroundMap
            = new Dictionary<int, SolidColorBrush>
            {
                {0, Brushes.Gray},
                {2, Brushes.WhiteSmoke},
                {4, Brushes.LightYellow},
                {8, new SolidColorBrush(Color.FromRgb(247, 188, 80))},
                {16, Brushes.DarkOrange},
                {32, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {64, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {128, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {256, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {512, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {1024, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {2048, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {4096, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {8192, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {16384, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {32768, new SolidColorBrush(System.Windows.Media.Colors.Black)},
                {65536, new SolidColorBrush(System.Windows.Media.Colors.Black)}
            };
    }
}