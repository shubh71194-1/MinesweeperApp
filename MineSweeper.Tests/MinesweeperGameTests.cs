using MineSweeper.Models;
using MineSweeper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MineSweeper.Tests
{
    public class MinesweeperGameTests
    {
        [Fact]
        public void MinesweeperGame_Initialize_ValidInputs_Success()
        {
            // Arrange
            var game = new MinesweeperGame();

            // Act
            game.Initialize(3, 2);

            // Assert
            Assert.Equal(3, game.GridSize);
        }

        [Fact]
        public void MinesweeperGame_RevealCell_MineAtCell_GameOver()
        {
            // Arrange
            var game = new MinesweeperGame();
            game.Initialize(3, 9);

            // Act
            game.RevealCell(0, 0);

            // Assert
            Assert.True(game.IsGameOver());
        }

        [Fact]
        public void MinesweeperGame_RevealCell_MineAtCell_GameWon()
        {
            // Arrange
            var game = new MinesweeperGame();
            game.Initialize(3, 0);

            // Act
            game.RevealCell(0, 0);

            // Assert
            Assert.True(game.IsGameWon());
        }

        [Fact]
        public void MinesweeperGame_GetMinefieldDisplay_ReturnsCorrectDisplay()
        {
            // Arrange
            var game = new MinesweeperGame();
            game.Initialize(3, 0); // Assuming a 3x3 grid for simplicity

            // Set up revealed cells
            game.RevealCell(0, 0);

            // Act
            string display = game.GetMinefieldDisplay();

            // Assert
            Assert.Equal("\n  1 2 3 \nA 0 0 0 \nB 0 0 0 \nC 0 0 0 \n\n", display);
        }
    }
}
