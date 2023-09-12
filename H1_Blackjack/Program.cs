namespace H1_Blackjack
{
    internal class Program
    {
        /// <summary>
        /// Calls the controller
        /// </summary>
        static void Main()
        {
            Controller.Controller controller = new Controller.Controller();
            controller.Start();
        }
    }
}