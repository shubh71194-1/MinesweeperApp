using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Views
{
    /// <summary>
    /// Console model to handle user input and display output
    /// </summary>
    public class ConsoleView : IView
    {
        public void DisplayMessage(string message)
        {
            Console.Write(message);
        }

        public string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}
