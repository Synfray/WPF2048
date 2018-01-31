using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF2048.Assets
{
    internal class GameSettings
    {

        // width and depth of Spielfeld in px
        public const int ConstSpielfeldSize = 500;

        // value and count of numbers at start of the game
        public const int StartValue = 2;
        public const int StartValueCount = 13;

        // #elements per row / column
        public const int ElementRoot = 4;
    }
}
