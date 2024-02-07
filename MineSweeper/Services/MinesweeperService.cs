using MineSweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Services
{
    public class MinesweeperService : IMinesweeperService
    {
        private readonly MinesweeperGame _game;

        /// <summary>
        /// Minesweeper Service to handle MinesweeperGame using constructor injection
        /// </summary>
        /// <param name="game"></param>
        public MinesweeperService(MinesweeperGame game)
        {
            _game = game;
        }

        public void InitializeGame(int gridSize, int numMines)
        {
            _game.Initialize(gridSize, numMines);
        }

        public void RevealCell(int row, int col)
        {
            _game.RevealCell(row, col);
        }

        public bool IsGameOver()
        {
            return _game.IsGameOver();
        }

        public bool IsGameWon()
        {
            return _game.IsGameWon();
        }

        public string GetMinefieldDisplay()
        {
            return _game.GetMinefieldDisplay();
        }

        public int GetGridSize()
        {
            return _game.GridSize;
        }
    }
}
