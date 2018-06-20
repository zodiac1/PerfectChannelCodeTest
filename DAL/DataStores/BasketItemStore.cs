using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Entities.Interfaces;

using Logging;

namespace DataAccess.DataStores
{
    /// <summary>
    /// Holds information about the item(s) in a user's basket
    /// </summary>
    public class BasketItemStore : IDataStore<IBasketItem>
    {
        private IList<IBasketItem> basketItems;

        public BasketItemStore()
        {
            basketItems = new List<IBasketItem>();
        }

        public BasketItemStore(IList<IBasketItem> basketItems)
        {
            this.basketItems = basketItems;
        }

        public string Id { get; set; }

        public IEnumerable<IBasketItem> GetAll()
        {
            return basketItems;
        }

        public IBasketItem Find(object id)
        {
            int idInt = (int)id;
            IBasketItem basketItem = basketItems.Single(x => x.ItemId == idInt);

            return basketItem;
        }

        public void Add(IBasketItem basketItem)
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Adding item '" + basketItem.ItemId + "' to the basket", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            basketItems.Add(basketItem);
        }

        public void Remove(IBasketItem basketItem)
        {
            basketItems.Remove(basketItem);
        }

        public void Update(IBasketItem basketItem)
        {
            throw new NotImplementedException();
        }

        public void Save(IBasketItem basketItem)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            basketItems.Clear();
        }

        public int Count()
        {
            return basketItems.Count;
        }
    }
}