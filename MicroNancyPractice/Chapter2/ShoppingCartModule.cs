using Nancy;
using Nancy.ModelBinding;

namespace Chapter2
{
    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule(IShoppingCartStore shoppingCartStore, IProductCatalogClient productCatalogClient,
            IEventStore eventStore) : base("/shoppingcart")
        {
            Get("/{userId:int}", parameters =>
            {
                var userId = (int) parameters.userid;
                return shoppingCartStore.Get(userId);
            });

            Post("/{userId:int}/items", async (parameters, _) =>
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int) parameters.userid;

                var shoppingCart = shoppingCartStore.Get(userId);
                var shoppingCartItems = await
                    productCatalogClient.GetShoppingCartItems(productCatalogIds).ConfigureAwait(false);
                shoppingCart.AddItems(shoppingCartItems, eventStore);
                shoppingCartStore.Save(shoppingCart);
                return shoppingCart;
            });

            Delete("/{userid:int}/items", parameters =>
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int) parameters.userid;

                var shoppingCart = shoppingCartStore.Get(userId);
                shoppingCart.RemoveItems(productCatalogIds, eventStore);
                shoppingCartStore.Save(shoppingCart);
                return shoppingCart;
            });
        }
    }
}