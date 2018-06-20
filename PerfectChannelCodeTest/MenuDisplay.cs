using System;

namespace PerfectChannelCodeTest
{
    public static class MenuDisplay
    {
        public static string DisplayMenu()
        {
            Console.WriteLine("[Please choose from the following options:]");
            Console.WriteLine();
            Console.WriteLine("1 - To see all items");
            Console.WriteLine("2 - To see all available items [Stock > 0]");
            Console.WriteLine("3 [Username] [Item Id or Name] [Quantity]- To add the specified quantity of items to the user's basket e.g. 1 Belal Chocolate 3. " +
                "\nThis option will create a new user if that user does not exist.");

            Console.WriteLine("4 [Username] - To remove all items in the user's basket e.g. 4 Belal. Please leave out the username to apply to all users.");
            Console.WriteLine("5 [Username] - To view the items in the user's basket e.g. 5 Belal.");
            Console.WriteLine("6 [Username] - To checkout/buy the items in the user's basket e.g. 6 Belal. Please leave out the username to apply to all users.");
            Console.WriteLine("7 - Exit");
            Console.WriteLine();

            string result = Console.ReadLine();
            return result;
        }
    }
}