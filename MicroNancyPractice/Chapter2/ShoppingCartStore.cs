using System.Collections.Generic;

namespace Chapter2
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private readonly Dictionary<int, ShoppingCart> _carts = new Dictionary<int, ShoppingCart>();

        public ShoppingCart Get(int userId)
        {
            if (!_carts.ContainsKey(userId)) _carts[userId] = ShoppingCart.CreateNew(userId);

            return _carts[userId].CopyToNew();
        }

        public void Save(ShoppingCart shoppingCart)
        {
            _carts[shoppingCart.UserId] = shoppingCart.CopyToNew();
        }
    }
}