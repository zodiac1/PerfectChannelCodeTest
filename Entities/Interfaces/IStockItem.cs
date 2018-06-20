namespace Entities.Interfaces
{
    public interface IStockItem
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Stock { get; set; }
        double Price { get; set; }
    }
}