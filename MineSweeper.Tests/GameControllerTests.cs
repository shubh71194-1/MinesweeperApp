using MineSweeper.Controllers;
using MineSweeper.Models;
using MineSweeper.Services;
using MineSweeper.Views;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MineSweeper.Tests
{
    public class GameControllerTests
    {

        [Fact]
        public void GameController_StartNewGame_GameOver_DisplayGameOverMessage()
        {
            // Arrange
            var viewMock = new Mock<IView>();
            viewMock.SetupSequence(v => v.GetUserInput())
                .Returns("3").Returns("2").Returns("A1");

            var serviceMock = new Mock<IMinesweeperService>();
            serviceMock.Setup(s => s.GetGridSize()).Returns(3);
            serviceMock.SetupSequence(s => s.IsGameOver())
                .Returns(false).Returns(true);

            var controller = new GameController(viewMock.Object, serviceMock.Object);

            // Act
            controller.StartNewGame();

            // Assert
            viewMock.Verify(v => v.DisplayMessage("Oh no, you detonated a mine! Game over.\n\n"), Times.Once);
        }

        [Fact]
        public void GameController_StartNewGame_GameWon_DisplayGameWonMessage()
        {
            // Arrange
            var viewMock = new Mock<IView>();
            viewMock.SetupSequence(v => v.GetUserInput())
                .Returns("3").Returns("2").Returns("A1");

            var serviceMock = new Mock<IMinesweeperService>();
            serviceMock.Setup(s => s.GetGridSize()).Returns(3);
            serviceMock.SetupSequence(s => s.IsGameOver())
                .Returns(false);
            serviceMock.Setup(s => s.IsGameWon()).Returns(true);

            var controller = new GameController(viewMock.Object, serviceMock.Object);

            // Act
            controller.StartNewGame();

            // Assert
            viewMock.Verify(v => v.DisplayMessage("Congratulations, you have won the game!\n\n"), Times.Once);
        }

        [Fact]
        public void GameController_StartNewGame_GetSelectedCell_DisplayIncorrectInput()
        {
            // Arrange
            var viewMock = new Mock<IView>();
            viewMock.SetupSequence(v => v.GetUserInput())
                .Returns("3").Returns("2")
                .Returns("A").Returns("A123")
                .Returns("!1").Returns("K1").Returns("A0").Returns("A11")
                .Returns("A1");

            var serviceMock = new Mock<IMinesweeperService>();
            serviceMock.Setup(s => s.GetGridSize()).Returns(3);
            serviceMock.SetupSequence(s => s.IsGameOver())
                .Returns(false).Returns(true);

            var controller = new GameController(viewMock.Object, serviceMock.Object);

            // Act
            controller.StartNewGame();

            // Assert
            viewMock.Verify(v => v.DisplayMessage("Incorect input.\n"), Times.Exactly(6));
        }

        [Fact]
        public void GameController_StartNewGame_GetGridSize_DisplayIncorrectInput()
        {
            // Arrange
            var viewMock = new Mock<IView>();
            viewMock.SetupSequence(v => v.GetUserInput())
                .Returns("1").Returns("11").Returns("A")
                .Returns("3").Returns("2")
                .Returns("A1");

            var serviceMock = new Mock<IMinesweeperService>();
            serviceMock.Setup(s => s.GetGridSize()).Returns(3);
            serviceMock.SetupSequence(s => s.IsGameOver())
                .Returns(false).Returns(true);

            var controller = new GameController(viewMock.Object, serviceMock.Object);

            // Act
            controller.StartNewGame();

            // Assert
            viewMock.Verify(v => v.DisplayMessage($"Minimum size of grid is {2}.\n"), Times.Once);
            viewMock.Verify(v => v.DisplayMessage($"Maximum size of grid is {10}.\n"), Times.Once);
            viewMock.Verify(v => v.DisplayMessage("Incorect input.\n"), Times.Once);
        }

        [Fact]
        public void GameController_StartNewGame_GetNumberOfMines_DisplayIncorrectInput()
        {
            // Arrange
            var viewMock = new Mock<IView>();
            viewMock.SetupSequence(v => v.GetUserInput())
                .Returns("3")
                .Returns("0").Returns("4").Returns("A")
                .Returns("3")
                .Returns("A1");

            var serviceMock = new Mock<IMinesweeperService>();
            serviceMock.Setup(s => s.GetGridSize()).Returns(3);
            serviceMock.SetupSequence(s => s.IsGameOver())
                .Returns(false).Returns(true);

            var controller = new GameController(viewMock.Object, serviceMock.Object);

            // Act
            controller.StartNewGame();

            // Assert
            viewMock.Verify(v => v.DisplayMessage("There must be at least 1 mine.\n"), Times.Once);
            viewMock.Verify(v => v.DisplayMessage($"Maximum number is 35% of total squares. ({3})\n"), Times.Once);
            viewMock.Verify(v => v.DisplayMessage("Incorect input.\n"), Times.Once);
        }
    }
}
