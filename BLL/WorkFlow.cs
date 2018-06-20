using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Entities;
using Entities.Interfaces;
using DataAccess.DataStores;

using Logging;

namespace BusinessLogic
{
    public class WorkFlow
    {
        private IDataStore<IDataStore<IBasketItem>> userBasketItemStore = new UserBasketItemStore();
        private IDataStore<IUser> userStore = new UserStore();

        private IDataStore<IStockItem> stockItemPersistentStore = new StockItemStore();
        private IDataStore<IStockItem> stockItemTempStore = new StockItemStore();

        public IEnumerable<IStockItem> GetAllStock()
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Retrieving all stock items...", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            return stockItemTempStore.GetAll();
        }

        public IEnumerable<IStockItem> GetAvailableStock()
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Retrieving available stock items...", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            IEnumerable<IStockItem> allStock = GetAllStock();
            IEnumerable<IStockItem> availableStock = allStock.Where(x => x.Stock > 0);

            return availableStock;
        }

        public bool AddItemToBasket(string username, object stockIdOrName, int quantity)
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Adding " + quantity + " item(s) of id/name '" + stockIdOrName + "' to the basket for user '" + username + "'", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            //Identify stock
            IStockItem stockItem = stockItemTempStore.Find(stockIdOrName);

            if (stockItem == null)
                return false;
            else
            {
                //Check stock availability
                if (stockItem.Stock < quantity)
                    return false;
            }

            //Check if user exists
            IUser user = userStore.Find(username);

            if (user == null)
            {
                user = new User(username);
                userStore.Add(user);
            }

            //Try to get the user's basket
            IDataStore<IBasketItem> basketItemStore = userBasketItemStore.Find(username);

            if (basketItemStore == null)
            {
                basketItemStore = new BasketItemStore();
                basketItemStore.Id = username;
            }

            //Add the item to the basket
            IBasketItem basketItem = new BasketItem(stockItem.Id, quantity);

            basketItemStore.Add(basketItem);
            userBasketItemStore.Add(basketItemStore);

            //Remove stock from the repository/update the stock count
            stockItemTempStore.Update(new StockItem(stockItem.Id, stockItem.Name, stockItem.Description, stockItem.Stock - quantity, stockItem.Price));

            return true;
        }

        public IEnumerable<IBasketItem> GetUserBasket(string username)
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Retrievig basket for user '" + username + "'", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            //Try to get the user's basket
            IDataStore<IBasketItem> basketItemStore = userBasketItemStore.Find(username);

            if (basketItemStore != null)
                return basketItemStore.GetAll();
            else
                return null;
        }

        public bool CheckoutUserBasket(string username)
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Checking out basket for user '" + username + "'", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            //Try to get the user's basket
            IDataStore<IBasketItem> basketItemStore = userBasketItemStore.Find(username);

            if (basketItemStore != null && basketItemStore.Count() > 0)
            {
                var userBasketItems = basketItemStore.GetAll();

                foreach(var userBasketItem in userBasketItems)
                {
                    stockItemPersistentStore.Refresh();
                    var stockItemPersistent = stockItemPersistentStore.Find(userBasketItem.ItemId);

                    int newStockCount = stockItemPersistent.Stock - userBasketItem.Quantity;

                    if (newStockCount >= 0)
                    {
                        stockItemTempStore.Save(new StockItem(userBasketItem.ItemId, stockItemPersistent.Name, stockItemPersistent.Description, newStockCount, stockItemPersistent.Price));
                    }
                    else
                        return false;
                }

                return true;
            }
            else
                return false;
        }

        public bool CheckoutAllUserBaskets()
        {
            if (userBasketItemStore.Count() > 0)
            {
                var userBasketItems = userBasketItemStore.GetAll();

                foreach(var userBasketItem in userBasketItems)
                {
                    bool success = CheckoutUserBasket(userBasketItem.Id);

                    if (!success)
                        return false;
                }

                return true;
            }
            else
                return false;
        }

        public bool ClearUserBasket(string username)
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Removing all items in basket for user '" + username + "'", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            //Try to get the user's basket
            IDataStore<IBasketItem> basketItemStore = userBasketItemStore.Find(username);

            if (basketItemStore != null && basketItemStore.Count() > 0)
            {
                var basketItems = basketItemStore.GetAll();

                foreach(var basketItem in basketItems)
                {
                    var stockItem = stockItemTempStore.Find(basketItem.ItemId);
                    stockItemTempStore.Update(new StockItem(basketItem.ItemId, stockItem.Name, stockItem.Description, stockItem.Stock + basketItem.Quantity, stockItem.Price));
                }

                basketItemStore.Reset();

                return true;
            }
            else
                return false;
        }

        public bool ClearAllUserBaskets()
        {
            if (userBasketItemStore.Count() > 0)
            {
                var userBasketItems = userBasketItemStore.GetAll();

                foreach (var userBasketItem in userBasketItems)
                {
                    bool success = ClearUserBasket(userBasketItem.Id);

                    if (!success)
                        return false;
                }

                return true;
            }
            else
                return false;
        }
    }
}