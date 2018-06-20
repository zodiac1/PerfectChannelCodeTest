using Entities;
using Entities.Interfaces;
using Persistence.DB;

using System.Collections.Generic;

namespace Persistence.Repositories
{
    public class StockItemsRepository : IRepository<IStockItem>
    {
        public IEnumerable<IStockItem> GetAll()
        {
            IList<IStockItem> items = new List<IStockItem>();

            CRUD.Read("Items", 
                (rdr)=>
                    {
                        items.Add
                            (
                                new StockItem
                                (
                                    rdr.GetInt32(0),            //Id
                                    rdr.GetString(1),           //Name
                                    rdr.GetString(2),           //Description
                                    rdr.GetInt32(3),            //Stock
                                    rdr.GetDouble(4)            //Price
                                )
                            );
                    }
                );
            
            return items;
        }

        public int Update(IStockItem item)
        {
            int result = CRUD.Update("UPDATE Items SET Stock = " + item.Stock + " WHERE Id = " + item.Id);
            return result;
        }
    }
}