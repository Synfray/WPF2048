using System.Collections.ObjectModel;

namespace WPF2048.Assets
{
    public static class EnumerationHelper
    {
        public static T Get2DElement<T>(this ObservableCollection<T> self, int x, int y, int d)
        {
            return self[y * d + x];
        }
    }
}