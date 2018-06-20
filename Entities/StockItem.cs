using Entities.Interfaces;

namespace Entities
{
    public class StockItem : IStockItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

        public StockItem(int id, string name, string description, int stock, double price)
        {
            Id = id;
            Name = name;
            Description = description;
            Stock = stock;
            Price = price;
        }
    }
}