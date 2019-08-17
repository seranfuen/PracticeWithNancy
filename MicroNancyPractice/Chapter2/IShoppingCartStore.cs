namespace Chapter2
{
    public interface IShoppingCartStore
    {
        ShoppingCart Get(int userId);
        void Save(ShoppingCart shoppingCart);
    }
}