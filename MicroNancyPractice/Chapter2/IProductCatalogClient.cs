using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chapter2
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds);
    }
}