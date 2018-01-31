using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BlurryControls.DialogFactory;
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
            SpawnElements(Assets.GameSettings.StartValueCount, Assets.GameSettings.StartValue);
        }

        #region Public Methods

        public void KeyAction(Direction direction)
        {
            // move and spawn
            switch (direction)
            {
                case Direction.Up:
                    Debug.WriteLine($"{direction} has been pressed.");
                    if(Move(true, 1))
                        SpawnElements(1,2);
                    break;
                case Direction.Down:
                    Debug.WriteLine($"{direction} has been pressed.");
                    if(Move(true, -1))
                        SpawnElements(1,2);
                    break;
                case Direction.Right:
                    Debug.WriteLine($"{direction} has been pressed.");
                    if(Move(false, -1))
                        SpawnElements(1,2);
                    break;
                case Direction.Left:
                    Debug.WriteLine($"{direction} has been pressed.");
                    if(Move(false, 1))
                        SpawnElements(1,2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            // lose
            if(!MovesPossible())
                BlurBehindMessageBox.Show("Cry, baby, cry");

            foreach (var element in _spielfeldElementViewModels)
            {
                if (element.Value == 2048)
                    BlurBehindMessageBox.Show("Well done");
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

        private bool MovesPossible()
        {
            return MovesPossible(true, 1) || MovesPossible(true, -1) || MovesPossible(false, 1) || MovesPossible(false, -1);
        }

        // this is generally move() without actually moving
        private bool MovesPossible(bool vertical, int dir)
        {
            var startIndex = dir == 1 ? 0 : Assets.GameSettings.ElementRoot - 1;
            var endIndex = dir == 1 ? Assets.GameSettings.ElementRoot - 1 : 0;
            var moved = false;

            // for each column
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
                    for (var i = startIndex; i != endIndex;)
                    {
                        var currentElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value;
                        var nextElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value;

                        if (currentElement == 0 && nextElement != 0)
                        {
                            // (don't) move
                            
                            moved = true;
                        }

                        i = i + dir;
                    }
                }


                // merge elements
                for (var i = startIndex; i != endIndex;)
                {
                    var currentElement = _spielfeldElementViewModels
                        .Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value;
                    var nextElement = _spielfeldElementViewModels
                        .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value;

                    if (currentElement != 0 && currentElement == nextElement)
                    {
                        // (don't) merge
                        
                        moved = true;
                    }

                    i = i + dir;
                }

                // move elements
                for (var repeat = 0; repeat < Assets.GameSettings.ElementRoot - 1; repeat++)
                {
                    // move elements once
                    for (var i = startIndex; i != endIndex;)
                    {
                        var currentElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value;
                        var nextElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value;

                        if (currentElement == 0 && nextElement != 0)
                        {
                            // (don't) move
                            
                            moved = true;
                        }

                        i = i + dir;
                    }
                }
            }

            return moved;
        }

        private bool Move(bool vertical, int dir)
        {
            var startIndex = dir == 1 ? 0 : Assets.GameSettings.ElementRoot - 1;
            var endIndex = dir == 1 ? Assets.GameSettings.ElementRoot - 1 : 0;
            var moved = false;

            // for each column
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
                    for (var i = startIndex; i != endIndex;)
                    {
                        var currentElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value;
                        var nextElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value;

                        if (currentElement == 0 && nextElement != 0)
                        {
                            // move

                            // this = next
                            _spielfeldElementViewModels.Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value =
                                nextElement;
                            // next = 0
                            _spielfeldElementViewModels
                                .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value = 0;

                            moved = true;
                        }

                        i = i + dir;
                    }
                }


                // merge elements
                for (var i = startIndex; i != endIndex;)
                {
                    var currentElement = _spielfeldElementViewModels
                        .Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value;
                    var nextElement = _spielfeldElementViewModels
                        .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value;

                    if (currentElement != 0 && currentElement == nextElement)
                    {
                        // merge

                        // this *2
                        _spielfeldElementViewModels.Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value =
                            nextElement + currentElement;
                        // next = 0
                        _spielfeldElementViewModels.Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j)
                            .Value = 0;

                        moved = true;
                    }

                    i = i + dir;
                }

                // move elements
                for (var repeat = 0; repeat < Assets.GameSettings.ElementRoot - 1; repeat++)
                {
                    // move elements once
                    for (var i = startIndex; i != endIndex;)
                    {
                        var currentElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value;
                        var nextElement = _spielfeldElementViewModels
                            .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value;

                        if (currentElement == 0 && nextElement != 0)
                        {
                            // move

                            // this = next
                            _spielfeldElementViewModels.Get2DElement(x: vertical ? j : i, y: vertical ? i : j).Value =
                                nextElement;
                            // next = 0
                            _spielfeldElementViewModels
                                .Get2DElement(x: vertical ? j : i + dir, y: vertical ? i + dir : j).Value = 0;

                            moved = true;
                        }

                        i = i + dir;
                    }
                }
            }

            return moved;
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
        private void SpawnElements(int count, int value)
        {
            var randomNumbers = new List<int>();
            var random = new Random();

            if (count > _elementCount)
                throw new ArgumentOutOfRangeException();

            while (randomNumbers.Count < count)
            {
                var rnd = random.Next(0, _elementCount);
                if (!randomNumbers.Contains(rnd) && _spielfeldElementViewModels[rnd].Value == 0)
                    randomNumbers.Add(rnd);
            }

            foreach (var randomNumber in randomNumbers)
                _spielfeldElementViewModels[randomNumber].Value = value;
        }

        #endregion
    }
}