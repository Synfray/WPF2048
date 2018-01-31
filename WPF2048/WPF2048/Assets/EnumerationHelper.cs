using System;
using System.Collections.ObjectModel;

namespace WPF2048.Assets
{
    public static class EnumerationHelper
    {
        public static T Get2DElement<T>(this ObservableCollection<T> self, int x, int y)
        {
            if (y >= GameSettings.ElementRoot || x >= GameSettings.ElementRoot)
                throw new IndexOutOfRangeException("omg die!");

            return self[y * GameSettings.ElementRoot + x];
        }
    }
}