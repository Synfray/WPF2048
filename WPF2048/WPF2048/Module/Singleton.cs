using WPF2048.ViewModel;

namespace WPF2048.Module
{
    public class Singleton
    {
        #region Fields

        private static SpielfeldViewModel _spielfeldViewModel;

        #endregion

        private Singleton()
        {
        }

        #region ViewModels

        public static SpielfeldViewModel SpielfeldViewModel =>
            _spielfeldViewModel ?? (_spielfeldViewModel = new SpielfeldViewModel());

        #endregion
    }
}