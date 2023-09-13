using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_Blackjack.View
{
    internal class View
    {
        /// <summary>
        /// Outputs a message, which is defined when the method is called
        /// </summary>
        /// <param name="message"></param>
        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Clears the console and outputs a message that is defined when the method is called
        /// </summary>
        /// <param name="message"></param>
        public void ClearMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }

        /// <summary>
        /// Outputs a message in green text, which is defined when the method is called
        /// This is used for player information.
        /// </summary>
        /// <param name="message"></param>
        public void GreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Outputs a message in red text, which is defined when the message is called.
        /// This is used for dealer information.
        /// </summary>
        /// <param name="message"></param>
        public void RedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Outputs a message, as well as waiting for the user to press enter to progress the program.
        /// </summary>
        /// <param name="message"></param>
        public void UserInput(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
