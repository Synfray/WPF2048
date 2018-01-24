using GalaSoft.MvvmLight;

namespace WPF2048.ViewModel
{
    public class SpielfeldElementViewModel : ViewModelBase
    {
        private int _elementSize;
        private int _fontSize = 30; // TODO adjust according to value
        private int _value;

        public SpielfeldElementViewModel(int elementSize)
        {
            _value = 0;
            _elementSize = elementSize;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        #region Properties

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        public int ElementSize
        {
            get => _elementSize;
            set
            {
                _elementSize = value;
                RaisePropertyChanged();
            }
        }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}