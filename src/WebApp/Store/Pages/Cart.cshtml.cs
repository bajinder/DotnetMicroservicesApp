using System;
using System.Linq;
using System.Threading.Tasks;
using Store.ApiCollection.Interfaces;
using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Store
{
    public class CartModel : PageModel
    {
        private readonly IBasketApi _basketApi;

        public CartModel(IBasketApi basketApi)
        {
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
        }

        public BasketModel Cart { get; set; } = new BasketModel();        

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketApi.GetBasket("bajinder");            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });
            //var product = await _catalogApi.GetCatalog(productId);

            var basket = await _basketApi.GetBasket("bajinder");

            var item = basket.Items.Single(item => item.ProductId == productId);
            basket.Items.Remove(item);

            await _basketApi.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}