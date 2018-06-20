using Entities.Interfaces;

namespace Entities
{
    public class BasketItem : IBasketItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public BasketItem(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}