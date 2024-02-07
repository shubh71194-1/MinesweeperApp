using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Models
{
    /// <summary>
    /// MinesweeperGame Model
    /// </summary>
    public class MinesweeperGame
    {
        // 2D array to store mines and adjcent mine count in one variable
        private int[,] _minefield;
        private bool[,] _revealedCells;
        private bool _gameOver;
        private int _gridSize;
        private int _numMines;
        private int _numRevealedCells;

        public int GridSize => _gridSize;

        /// <summary>
        /// Game object initialization
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="numMines"></param>
        public void Initialize(int gridSize, int numMines)
        {
            _gridSize = gridSize;
            _numMines = numMines;
            _minefield = new int[_gridSize, _gridSize];
            _revealedCells = new bool[_gridSize, _gridSize];
            _gameOver = false;
            _numRevealedCells = 0;

            InitializeMines();
            PrecomputeAdjacentMineCounts();
        }

        /// <summary>
        /// Placing Mines in Grid
        /// </summary>
        private void InitializeMines()
        {
            Random random = new Random();
            int remainingMines = _numMines;

            while (remainingMines > 0)
            {
                int row = random.Next(_gridSize);
                int col = random.Next(_gridSize);

                if (_minefield[row, col] != Constants.Constants.MineValue)
                {
                    _minefield[row, col] = Constants.Constants.MineValue;
                    remainingMines--;
                }
            }
        }

        /// <summary>
        /// Calculating adjcent mine count for each cell in grid
        /// </summary>
        private void PrecomputeAdjacentMineCounts()
        {
            for (int row = 0; row < _gridSize; row++)
            {
                for (int col = 0; col < _gridSize; col++)
                {
                    if (_minefield[row, col] == Constants.Constants.MineValue) continue;
                    _minefield[row, col] = CountAdjacentMines(row, col);
                }
            }
        }

        /// <summary>
        /// Calculating adjcent mine count for cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private int CountAdjacentMines(int row, int col)
        {
            int count = 0;
            for (int dr = Math.Max(row - 1, 0); dr <= Math.Min(row + 1, _gridSize - 1); dr++)
            {
                for (int dc = Math.Max(col - 1, 0); dc <= Math.Min(col + 1, _gridSize - 1); dc++)
                {
                    if (dr == row && dc == col) continue;
                    if (_minefield[dr, dc] == Constants.Constants.MineValue) count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Reveal selected cells
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void RevealCell(int row, int col)
        {
            if (_revealedCells[row, col]) return;

            // Game over for mine field selection
            if (_minefield[row, col] == Constants.Constants.MineValue)
            {
                _gameOver = true;
            }
            else
            {
                _revealedCells[row, col] = true;
                _numRevealedCells++;

                // Reveal adjacent cells for 'zero' count cells
                if (_minefield[row, col] == Constants.Constants.EmptyMineValue)
                {
                    RevealAdjacentCells(row, col);
                }
            }
        }

        /// <summary>
        /// Reveal adjacent cells for cell with 'zero' mine count 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void RevealAdjacentCells(int row, int col)
        {
            for (int r = Math.Max(row - 1, 0); r <= Math.Min(row + 1, _gridSize - 1); r++)
            {
                for (int c = Math.Max(col - 1, 0); c <= Math.Min(col + 1, _gridSize - 1); c++)
                {
                    if (r == row && c == col) continue;
                    if (!_revealedCells[r,c])
                        RevealCell(r, c);
                }
            }
        }

        public bool IsGameOver()
        {
            return _gameOver;
        }

        public bool IsGameWon()
        {
            return _gameOver = _numRevealedCells == _gridSize * _gridSize - _numMines;
        }

        /// <summary>
        /// Display grid of mine field
        /// </summary>
        /// <returns></returns>
        public string GetMinefieldDisplay()
        {
            string display = "\n  ";
            for (int col = 0; col < _gridSize; col++)
            {
                display += $"{col + 1} ";
            }
            display += "\n";

            for (int row = 0; row < _gridSize; row++)
            {
                display += (char)(Constants.Constants.RowStartValue + row) + " ";
                for (int col = 0; col < _gridSize; col++)
                {
                    if (_revealedCells[row, col])
                    {
                        display += _minefield[row, col] == Constants.Constants.MineValue ? "*" : _minefield[row, col].ToString();
                    }
                    else
                    {
                        display += "_";
                    }
                    display += " ";
                }
                display += "\n";
            }
            display += "\n";

            return display;
        }
    }
}
