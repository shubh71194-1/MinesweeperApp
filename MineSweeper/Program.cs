using MineSweeper.Controllers;
using MineSweeper.Models;
using MineSweeper.Services;
using MineSweeper.Views;
using System;
using System.Collections.Generic;

namespace MinesweeperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Continue game execution
            while (true)
            {
                // Game starting point
                // Passing Game object to service and service to controller
                var game = new GameController(new ConsoleView(), new MinesweeperService(new MinesweeperGame()));
                game.StartNewGame();

                Console.WriteLine("Press any key to play again...");
                Console.ReadKey();
            }
        }
    }
}
