using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Entities.Interfaces;
using Persistence.Repositories;

using Logging;

namespace DataAccess.DataStores
{
    /// <summary>
    /// Holds information about stock items
    /// </summary>
    public class StockItemStore : IDataStore<IStockItem>
    {
        private IRepository<IStockItem> stockItemsRepository;
        private IDictionary<int, IStockItem> stockItems;

        public StockItemStore()
        {
            stockItemsRepository = new StockItemsRepository();
            stockItems = new Dictionary<int, IStockItem>();

            Refresh();
        }

        public StockItemStore(IRepository<IStockItem> stockItemsRepository, IDictionary<int, IStockItem> stockItems)
        {
            this.stockItemsRepository = stockItemsRepository;
            this.stockItems = stockItems;

            Refresh();
        }

        public string Id { get; set; }

        public IEnumerable<IStockItem> GetAll()
        {
            return stockItems.Values.ToList();
        }

        public IStockItem Find(object idOrName)
        {
            Log.Info(string.Format("[{0}][{1}][{2}] Searching for item: " + idOrName, Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

            string idOrNameString = idOrName.ToString();

            int idInt;
            bool success = false;

            success = int.TryParse(idOrNameString, out idInt);

            if(!success)
            {
                IStockItem stockItem = stockItems.Values.Where(x => x.Name == idOrNameString).SingleOrDefault();

                if(stockItem != null)
                    idInt = stockItem.Id;
            }
            
            if (stockItems.ContainsKey(idInt))
                return stockItems[idInt];
            else
                return null;
        }

        public void Add(IStockItem stockItem)
        {
            if (stockItems.ContainsKey(stockItem.Id))
            {
                stockItems[stockItem.Id] = stockItem;
            }
            else
                stockItems.Add(stockItem.Id, stockItem);
        }

        public void Remove(IStockItem stockItem)
        {
            if (stockItems.ContainsKey(stockItem.Id))
            {
                stockItems.Remove(stockItem.Id);
            }
        }

        public void Update(IStockItem stockItem)
        {
            if (stockItems.ContainsKey(stockItem.Id))
            {
                Log.Info(string.Format("[{0}][{1}][{2}] Updating stock: " + stockItem.Name, Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

                stockItems[stockItem.Id].Name = stockItem.Name;
                stockItems[stockItem.Id].Description = stockItem.Description;
                stockItems[stockItem.Id].Stock = stockItem.Stock;
                stockItems[stockItem.Id].Price = stockItem.Price;
            }
        }

        public void Save(IStockItem stockItem)
        {
            stockItemsRepository.Update(stockItem);
        }

        public void Refresh()
        {
            stockItems = stockItemsRepository.GetAll().ToDictionary(x => x.Id, x => x);
        }

        public void Reset()
        {
            stockItems.Clear();
        }

        public int Count()
        {
            return stockItems.Count;
        }
    }
}