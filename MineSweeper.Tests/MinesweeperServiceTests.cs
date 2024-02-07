using MineSweeper.Models;
using MineSweeper.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MineSweeper.Tests
{
    public class MinesweeperServiceTests
    {
        [Fact]
        public void MinesweeperService_InitializeGame_ValidInputs_Success()
        {
            // Arrange
            var gameMock = new Mock<MinesweeperGame>();
            var service = new MinesweeperService(gameMock.Object);

            // Act
            service.InitializeGame(3, 2);

            // Assert
            Assert.Equal(3, service.GetGridSize());
        }

        [Fact]
        public void MinesweeperService_RevealCell_MineAtCell()
        {
            // Arrange
            
            var game = new MinesweeperGame();
            game.Initialize(3, 9);
            var service = new MinesweeperService(game);

            // Act
            service.RevealCell(0, 0);

            // Assert
            Assert.True(service.IsGameOver());
        }

        [Fact]
        public void MinesweeperService_RevealCell_NoMineAtCell()
        {
            // Arrange

            var game = new MinesweeperGame();
            game.Initialize(3, 0);
            var service = new MinesweeperService(game);

            // Act
            service.RevealCell(0, 0);

            // Assert
            Assert.True(service.IsGameWon());
        }

        [Fact]
        public void MinesweeperService_GetMinefieldDisplay_ReturnsCorrectDisplay()
        {
            // Arrange
            var game = new MinesweeperGame();
            game.Initialize(3, 3);
            var service = new MinesweeperService(game);

            // Act
            string display = service.GetMinefieldDisplay();

            // Assert
            Assert.Equal("\n  1 2 3 \nA _ _ _ \nB _ _ _ \nC _ _ _ \n\n", display);
        }
    }
}
