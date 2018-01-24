using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using WPF2048.Assets;

namespace WPF2048.ViewModel
{
    public class SpielfeldViewModel : ViewModelBase
    {
        public SpielfeldViewModel()
        {
            InitializeParameters();
            InitializeSpielfeldElemente();
            InitializeStartValues();
        }

        #region Public Methods

        public void KeyAction(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Debug.WriteLine($"{direction} has been pressed.");
                    break;
                case Direction.Down:
                    Debug.WriteLine($"{direction} has been pressed.");
                    break;
                case Direction.Right:
                    Debug.WriteLine($"{direction} has been pressed.");
                    break;
                case Direction.Left:
                    Debug.WriteLine($"{direction} has been pressed.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        #endregion

        #region Fields

        // width and depth of Spielfeld in px
        public const int ConstSpielfeldSize = 500;
        private int _spielfeldSize;
        private int _elementSize;

        // value and count of numbers at start of the game
        public const int StartValue = 2;
        public const int StartValueCount = 2;

        // list of elements
        private ObservableCollection<SpielfeldElementViewModel> _spielfeldElementViewModels =
            new ObservableCollection<SpielfeldElementViewModel>();

        // #elements per row / column
        private const int ElementRoot = 4;
        // total #elements
        private int _elementCount; 

        #endregion

        #region Properties

        public ObservableCollection<SpielfeldElementViewModel> SpielfeldElementViewModels
        {
            get => _spielfeldElementViewModels;
            set
            {
                _spielfeldElementViewModels = value;
                RaisePropertyChanged();
            }
        }

        public int SpielfeldSize
        {
            get => _spielfeldSize;
            set
            {
                _spielfeldSize = value;
                RaisePropertyChanged(() => SpielfeldSize);
            }
        }

        public int ElementSize
        {
            get => _elementSize;
            set
            {
                _elementSize = value;
                RaisePropertyChanged(() => ElementSize);
            }
        }

        #endregion

        #region Private Methods

        private void InitializeParameters()
        {
            SpielfeldSize = ConstSpielfeldSize;
            ElementSize = ConstSpielfeldSize / ElementRoot;
            _elementCount = ElementRoot * ElementRoot;
        }

        private void InitializeSpielfeldElemente()
        {
            for (var i = 0; i < _elementCount; i++)
                _spielfeldElementViewModels.Add(new SpielfeldElementViewModel(ElementSize));
        }

        private void InitializeStartValues()
        {
            var randomNumbers = new List<int>();
            var random = new Random();

            if (StartValueCount > _elementCount)
                throw new ArgumentOutOfRangeException();

            while (randomNumbers.Count < StartValueCount)
            {
                var rnd = random.Next(0, _elementCount);
                if (!randomNumbers.Contains(rnd))
                    randomNumbers.Add(rnd);
            }

            foreach (var randomNumber in randomNumbers)
                _spielfeldElementViewModels[randomNumber].Value = StartValue;
        }

        #endregion
    }
}