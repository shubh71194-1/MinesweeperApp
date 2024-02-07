using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Views
{
    public interface IView
    {
        void DisplayMessage(string message);
        string GetUserInput();
    }
}
