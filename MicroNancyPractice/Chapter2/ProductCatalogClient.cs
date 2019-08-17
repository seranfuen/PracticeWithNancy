using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chapter2
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private static readonly string baseUrl = "http://private-29781-pruebasnancy.apiary-mock.com";
        private static readonly string productPathTemplate = "/products";

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds)
        {
            var response = await RequestProductFromProductCatalog(productCatalogIds);
            return await ConvertToShoppingCartItems(response);
        }

        private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(
            HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var contents = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
            var products =
                JsonConvert.DeserializeObject<List<Product>>(contents);

            return products.Select(p => new ShoppingCartItem
            {
                Price = p.Price,
                ProductDescription = p.ProductDescription,
                ProductId = p.ProductId,
                ProductName = p.ProductName
            }).ToList();
        }

        private static async Task<HttpResponseMessage> RequestProductFromProductCatalog(
            IEnumerable<int> productCatalogIds)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                return await httpClient.GetAsync("/products").ConfigureAwait(false);
            }
        }
    }
}