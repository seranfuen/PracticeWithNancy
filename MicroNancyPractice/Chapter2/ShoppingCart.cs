using System.Collections.Generic;

namespace Chapter2
{
    public class ShoppingCart
    {
        private ShoppingCart(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }

        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var item in shoppingCartItems)
            {
                Items.Add(item);
                eventStore.Raise("ShoppingCartItemAdded", new {UserId, item});
            }
        }

        public ShoppingCart CopyToNew()
        {
            return new ShoppingCart(UserId);
        }

        public static ShoppingCart CreateNew(int userId)
        {
            return new ShoppingCart(userId);
        }

        public void RemoveItems(int[] productCatalogIds, IEventStore eventStore)
        {
        }
    }
}