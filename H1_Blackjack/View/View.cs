using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_Blackjack.View
{
    internal class View
    {
        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public void ClearMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }

        public void GreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void RedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
