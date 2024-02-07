using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Constants
{
    public static class Constants
    {
        public const int MineValue = -1;
        public const int EmptyMineValue = 0;

        public const int MinimumGridSize = 2;
        public const int MaximumGridSize = 10;
        public const int MinimunNumberOfMines = 1;
        public const double MinePercentage = 0.35;

        public const int MinimunCellInputLength = 2;
        public const int MaximumCellInputLength = 3;

        public const char RowStartValue = 'A';
    }
}
