using System;

using Logging;

namespace PerfectChannelCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string input = "";

                do
                {
                    input = MenuDisplay.DisplayMenu();
                    MenuProcessor.ProcessMenuOption(input);
                } while (input.Trim() != "7");
            }
            catch(Exception ex)
            {
                Log.Error("An unexpected error occured. The error is: ", ex);

                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                Console.WriteLine();

                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();

                Environment.Exit(1);
            }
        }
    }
}
