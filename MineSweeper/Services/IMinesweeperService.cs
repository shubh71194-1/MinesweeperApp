using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Services
{
    public interface IMinesweeperService
    {
        void InitializeGame(int gridSize, int numMines);
        void RevealCell(int row, int col);
        bool IsGameOver();
        bool IsGameWon();
        string GetMinefieldDisplay();
        int GetGridSize();
    }
}
