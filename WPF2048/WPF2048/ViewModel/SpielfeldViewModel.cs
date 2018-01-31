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
                    Move(0, 1, 1, 1);
                    break;
                case Direction.Down:
                    Debug.WriteLine($"{direction} has been pressed.");
                    Move(0, Assets.GameSettings.ElementRoot - 2, 1, -1);
                    break;
                case Direction.Right:
                    Debug.WriteLine($"{direction} has been pressed.");
                    Move(Assets.GameSettings.ElementRoot - 2, 0, -1, 1);
                    break;
                case Direction.Left:
                    Debug.WriteLine($"{direction} has been pressed.");
                    Move(1, 0, 1, 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        #endregion

        #region Fields

        private int _spielfeldSize;
        private int _elementSize;

        // list of elements
        private ObservableCollection<SpielfeldElementViewModel> _spielfeldElementViewModels =
            new ObservableCollection<SpielfeldElementViewModel>();

        // total #elements
        private int _elementCount;

        #endregion

        #region Properties

        // "array" of game elements
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

        private void Move(int xStart, int yStart, int xDir, int yDir)
        {
            // for each row (l/r) or column (u/d)
            for (var j = 0; j < Assets.GameSettings.ElementRoot; j++)
            {
                // move - merge - move
                // move them together so nearest to <moveDir> get merged first
                // merge equal values. gaps can appear
                // move to close gaps


                // move elements
                for (var repeat = 0; repeat < Assets.GameSettings.ElementRoot - 1; repeat++)
                {
                    // move elements once
                    for (var i = 0; i < Assets.GameSettings.ElementRoot - 1; i++)
                    {
                        var currentElement = _spielfeldElementViewModels.Get2DElement(j, i).Value;
                        var nextElement = _spielfeldElementViewModels.Get2DElement(j, i + 1).Value;

                        if (currentElement == 0)
                        {
                            // move

                            // this = next
                            _spielfeldElementViewModels.Get2DElement(j, i).Value = nextElement;
                            // next = 0
                            _spielfeldElementViewModels.Get2DElement(j, i + 1).Value = 0;
                        }
                    }
                }


                // merge elements
                for (var i = 0; i < Assets.GameSettings.ElementRoot - 1; i++)
                {
                    var currentElement = _spielfeldElementViewModels.Get2DElement(j, i).Value;
                    var nextElement = _spielfeldElementViewModels.Get2DElement(j, i + 1).Value;

                    if (currentElement != 0 && currentElement == nextElement)
                    {
                        // merge

                        // this *2
                        _spielfeldElementViewModels.Get2DElement(j, i).Value = nextElement + currentElement;
                        // next = 0
                        _spielfeldElementViewModels.Get2DElement(j, i + 1).Value = 0;
                    }
                }

                // move elements
                for (var repeat = 0; repeat < Assets.GameSettings.ElementRoot - 1; repeat++)
                {
                    // move elements once
                    for (var i = 0; i < Assets.GameSettings.ElementRoot - 1; i++)
                    {
                        var currentElement = _spielfeldElementViewModels.Get2DElement(j, i).Value;
                        var nextElement = _spielfeldElementViewModels.Get2DElement(j, i + 1).Value;

                        if (currentElement == 0)
                        {
                            // move

                            // this = next
                            _spielfeldElementViewModels.Get2DElement(j, i).Value = nextElement;
                            // next = 0
                            _spielfeldElementViewModels.Get2DElement(j, i + 1).Value = 0;
                        }
                    }
                }
            }
        }

        // gets global parameters and sets them for the view
        private void InitializeParameters()
        {
            SpielfeldSize = Assets.GameSettings.ConstSpielfeldSize;
            ElementSize = Assets.GameSettings.ConstSpielfeldSize / Assets.GameSettings.ElementRoot;
            _elementCount = Assets.GameSettings.ElementRoot * Assets.GameSettings.ElementRoot;
        }

        // initializes elements
        private void InitializeSpielfeldElemente()
        {
            for (var i = 0; i < _elementCount; i++)
                _spielfeldElementViewModels.Add(new SpielfeldElementViewModel(ElementSize));
        }

        // creates elements with numbers at start of the game
        private void InitializeStartValues()
        {
            var randomNumbers = new List<int>();
            var random = new Random();

            if (Assets.GameSettings.StartValueCount > _elementCount)
                throw new ArgumentOutOfRangeException();

            while (randomNumbers.Count < Assets.GameSettings.StartValueCount)
            {
                var rnd = random.Next(0, _elementCount);
                if (!randomNumbers.Contains(rnd))
                    randomNumbers.Add(rnd);
            }

            foreach (var randomNumber in randomNumbers)
                _spielfeldElementViewModels[randomNumber].Value = Assets.GameSettings.StartValue;
        }

        #endregion
    }
}