using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.ApiCollection.Interfaces;
using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Store
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketApi _basketApi;

        public ProductModel(ICatalogApi productRepository, IBasketApi cartRepository)
        {
            _catalogApi = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _basketApi = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string? categoryName)
        {
            var productList = await _catalogApi.GetCatalog();
            CategoryList = productList.Select(p => p.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = productList.Where(p => p.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
            }

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