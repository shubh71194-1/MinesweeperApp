using MineSweeper.Models;
using MineSweeper.Services;
using MineSweeper.Views;
using MinesweeperApp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Controllers
{
    public class GameController
    {
        private readonly IView _view;
        private readonly IMinesweeperService _minesweeperService;

        /// <summary>
        /// Controller with view and minesweeper service injection
        /// </summary>
        /// <param name="view"></param>
        /// <param name="minesweeperService"></param>
        public GameController(IView view, IMinesweeperService minesweeperService)
        {
            _view = view;
            _minesweeperService = minesweeperService;
        }

        /// <summary>
        /// Start Game point
        /// </summary>
        public void StartNewGame()
        {
            _view.DisplayMessage("\n"+Resources.MessageResources.Welcome+"\n\n");

            int gridSize = GetGridSize();
            int numMines = GetNumberOfMines(gridSize);

            // initializing game object from service
            _minesweeperService.InitializeGame(gridSize, numMines);

            while (!_minesweeperService.IsGameOver())
            {
                DisplayMinefield();
                var position = GetSelectedCell();
                int row = position / gridSize;
                int col = position % gridSize;

                _minesweeperService.RevealCell(row, col);

                if (_minesweeperService.IsGameOver()) // check for game over
                {
                    RevealAllCells(gridSize);
                    _view.DisplayMessage(Resources.MessageResources.GameOverMessage+"\n\n");
                    break;
                }
                else if (_minesweeperService.IsGameWon()) // check for game won
                {
                    RevealAllCells(gridSize);
                    _view.DisplayMessage(Resources.MessageResources.GameWonMessage + "\n\n");
                    break;
                }
            }
        }

        /// <summary>
        /// Grid size input from user
        /// </summary>
        /// <returns></returns>
        private int GetGridSize()
        {
            const int minSize = Constants.Constants.MinimumGridSize;
            const int maxSize = Constants.Constants.MaximumGridSize;

            int gridSize;
            string input;
            do
            {
                _view.DisplayMessage(Resources.MessageResources.EnterGridSize+"\n");
                if (int.TryParse(input = _view.GetUserInput(), out gridSize))
                {
                    if (gridSize < minSize)
                    {
                        _view.DisplayMessage(Resources.MessageResources.MinGridSizeMessage+$" {minSize}.\n");
                    }
                    else if (gridSize > maxSize)
                    {
                        _view.DisplayMessage(Resources.MessageResources.MaxGridSizeMessage+$" {maxSize}.\n");
                    }
                }
                else
                {
                    _view.DisplayMessage(Resources.MessageResources.IncorrectInput+"\n");
                }
            } while (!int.TryParse(input, out gridSize) || gridSize < minSize || gridSize > maxSize);

            return gridSize;
        }

        /// <summary>
        /// Number of Mines input from user
        /// </summary>
        /// <param name="gridSize"></param>
        /// <returns></returns>
        private int GetNumberOfMines(int gridSize)
        {
            int minMines = Constants.Constants.MinimunNumberOfMines;
            int numMines;
            int maxMines = (int)(gridSize * gridSize * Constants.Constants.MinePercentage);

            string input;
            do
            {
                _view.DisplayMessage(Resources.MessageResources.EnterMineNumber + $" ({maxMines})):\n");

                if (int.TryParse(input = _view.GetUserInput(), out numMines))
                {
                    if (numMines < minMines)
                    {
                        _view.DisplayMessage(Resources.MessageResources.MinMineNumberMessage+$"\n");
                    }
                    else if (numMines > maxMines)
                    {
                        _view.DisplayMessage(Resources.MessageResources.MaxMineNumberMessage+$" ({maxMines})\n");
                    }
                }
                else
                {
                    _view.DisplayMessage(Resources.MessageResources.IncorrectInput + "\n");
                }
            } while (!int.TryParse(input, out numMines) || numMines < minMines || numMines > maxMines);

            return numMines;
        }

        private void DisplayMinefield()
        {
            _view.DisplayMessage("\n"+ Resources.MessageResources.MinefieldTitle);
            _view.DisplayMessage(_minesweeperService.GetMinefieldDisplay());
        }

        private void RevealAllCells(int gridSize)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    _minesweeperService.RevealCell(row, col);
                }
            }
            DisplayMinefield();
        }

        /// <summary>
        /// User input for cell selection
        /// </summary>
        /// <returns></returns>
        private int GetSelectedCell()
        {
            int position=0;
            string input;
            bool isValid;
            int gridSize = _minesweeperService.GetGridSize();
            do
            {
                _view.DisplayMessage(Resources.MessageResources.SelectSquareMessage);
                input = _view.GetUserInput().ToUpper();

                isValid = !(input.Length < Constants.Constants.MinimunCellInputLength || input.Length > Constants.Constants.MaximumCellInputLength);
                if (!isValid)
                {
                    _view.DisplayMessage(Resources.MessageResources.IncorrectInput + "\n");
                    continue;
                }

                int row = input[0] - Constants.Constants.RowStartValue;
                int col = int.Parse(input.Substring(1)) - 1;

                isValid = !(row < 0 || row >= gridSize || col < 0 || col >= gridSize);

                if (!isValid)
                {
                    _view.DisplayMessage(Resources.MessageResources.IncorrectInput + "\n");
                    continue;
                }

                position = row * _minesweeperService.GetGridSize() + col;

            } while (!isValid);

            return position;
        }
    }
}
