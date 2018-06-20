using System;
using System.Linq;
using System.Collections.Generic;

using Entities.Interfaces;

namespace DataAccess.DataStores
{
    /// <summary>
    /// Holds a mapping of users and baskets
    /// </summary>
    public class UserBasketItemStore : IDataStore<IDataStore<IBasketItem>>
    {
        private IDictionary<string, IDataStore<IBasketItem>> userBasketItems;

        public UserBasketItemStore()
        {
            userBasketItems = new Dictionary<string, IDataStore<IBasketItem>>();
        }

        public UserBasketItemStore(IDictionary<string, IDataStore<IBasketItem>> userBasketItems)
        {
            this.userBasketItems = userBasketItems;
        }

        public string Id { get; set; }

        public IEnumerable<IDataStore<IBasketItem>> GetAll()
        {
            return userBasketItems.Values.ToList();
        }

        public IDataStore<IBasketItem> Find(object username)
        {
            string usernameString = (string)username;

            if (userBasketItems.ContainsKey(usernameString))
                return userBasketItems[usernameString];
            else
                return null;
        }

        public void Add(IDataStore<IBasketItem> basketItemStore)
        {
            if (!userBasketItems.ContainsKey(basketItemStore.Id))
                userBasketItems.Add(basketItemStore.Id, basketItemStore);
            else
                userBasketItems[basketItemStore.Id] = basketItemStore;
        }

        public void Remove(IDataStore<IBasketItem> basketItemStore)
        {
            if (userBasketItems.ContainsKey(basketItemStore.Id))
                userBasketItems.Remove(basketItemStore.Id);
        }

        public void Update(IDataStore<IBasketItem> basketItemStore)
        {
            throw new NotImplementedException();
        }

        public void Save(IDataStore<IBasketItem> basketItemStore)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            userBasketItems.Clear();
        }

        public int Count()
        {
            return userBasketItems.Count;
        }
    }
}