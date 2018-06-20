using System;
using System.Linq;
using System.Collections.Generic;

using Entities.Interfaces;
using BusinessLogic;

namespace PerfectChannelCodeTest
{
    public static class MenuProcessor
    {
        private static WorkFlow workFlow = new WorkFlow();

        public static void ProcessMenuOption(string input)
        {
            string menuInput = input.Trim();
            int menuNumber = int.Parse(input.Substring(0, 1));      //ToDo: Use TryParse instead

            switch (menuNumber)
            {
                case 1:
                    var allItems = workFlow.GetAllStock();

                    Console.WriteLine();
                    Console.WriteLine("[Displaying all items:]");
                    Console.WriteLine();

                    DisplayStockItems(allItems);

                    break;
                case 2:
                    var availableItems = workFlow.GetAvailableStock();

                    Console.WriteLine();
                    Console.WriteLine("[Displaying available items:]");
                    Console.WriteLine();

                    DisplayStockItems(availableItems);

                    break;
                case 3:
                    {
                        string[] inputs = menuInput.Split(' ');

                        var isItemAddedSuccess = workFlow.AddItemToBasket(inputs[1], inputs[2], int.Parse(inputs[3]));

                        Console.WriteLine();

                        if (isItemAddedSuccess)
                            Console.WriteLine("Successfully added item to the basket.");
                        else
                            Console.WriteLine("Unable to add item to the basket. Please check that stock is available.");

                        Console.WriteLine();

                        break;
                    }
                case 4:
                    {
                        string[] inputs = menuInput.Split(' ');

                        bool isBasketClearSuccess = false;

                        if(inputs.Length > 1)
                            isBasketClearSuccess = workFlow.ClearUserBasket(inputs[1]);
                        else
                            isBasketClearSuccess = workFlow.ClearAllUserBaskets();

                        Console.WriteLine();

                        if (isBasketClearSuccess)
                            Console.WriteLine("Successfully removed all item(s) in the basket(s).");
                        else
                            Console.WriteLine("Unable to remove the item(s) from the basket(s).");

                        Console.WriteLine();

                        break;
                    }
                case 5:
                    {
                        string[] inputs = menuInput.Split(' ');
                        var userBasketItems = workFlow.GetUserBasket(inputs[1]);

                        if (userBasketItems != null)
                        {
                            if (userBasketItems.Count() > 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("[Displaying items in the basket:]");
                                Console.WriteLine();

                                DisplayBasketItems(userBasketItems);
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("There are no items in the basket belonging to '" + inputs[1] + "'.");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Unable to find basket for '" + inputs[1] + "'");
                            Console.WriteLine();
                        }

                        break;
                    }
                case 6:
                    {
                        string[] inputs = menuInput.Split(' ');

                        bool isCheckoutSuccess = false;

                        if(inputs.Length > 1)
                            isCheckoutSuccess = workFlow.CheckoutUserBasket(inputs[1]);
                        else
                            isCheckoutSuccess = workFlow.CheckoutAllUserBaskets();

                        Console.WriteLine();

                        if (isCheckoutSuccess)
                            Console.WriteLine("Successfully checked out the item(s) in the basket(s).");
                        else
                            Console.WriteLine("Unable to check out the item(s) in the basket(s). Please check that stock is available.");

                        Console.WriteLine();

                        break;
                    }
                case 7:
                    Environment.Exit(1);

                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Menu selection not recognised. Please check and try again.");
                    Console.WriteLine();

                    break;
            }
        }

        private static void DisplayStockItems(IEnumerable<IStockItem> stockItems)
        {
            foreach (var stockItem in stockItems)
            {
                DisplayStockItem(stockItem);
            }

            Console.WriteLine();
        }

        private static void DisplayBasketItems(IEnumerable<IBasketItem> basketItems)
        {
            foreach (var basketItem in basketItems)
            {
                DisplayBasketItem(basketItem);
            }

            Console.WriteLine();
        }

        private static void DisplayStockItem(IStockItem stockItem)
        {
            Console.WriteLine(string.Format("Id: {0}, Name: {1}, Description: {2}, Stock: {3}, Price: {4}", stockItem.Id, stockItem.Name, stockItem.Description, stockItem.Stock, stockItem.Price));
        }

        private static void DisplayBasketItem(IBasketItem basketItem)
        {
            var stockItem = workFlow.GetAllStock().Where(x => x.Id == basketItem.ItemId).SingleOrDefault();
            Console.WriteLine(string.Format("Id: {0}, Name: {1}, Quantity: {2}", basketItem.ItemId, stockItem.Name, basketItem.Quantity));
        }
    }
}
