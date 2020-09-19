using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.ApiCollection.Interfaces;
using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Store.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public IndexModel(ICatalogApi catalogApi, IBasketApi basketApi)
        {
            _catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogApi.GetCatalog();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });
            var product = await _catalogApi.GetCatalog(productId);

            var basket = await _basketApi.GetBasket("bajinder");

            basket.Items.Add(new BasketItemModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Color = "black",
                Quantity = 1
            }); ;

            await _basketApi.UpdateBasket(basket);
            
            return RedirectToPage("Cart");
        }
    }
}
