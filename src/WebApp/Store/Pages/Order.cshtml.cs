using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.ApiCollection.Interfaces;
using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Store
{
    public class OrderModel : PageModel
    {
        private readonly IOrderApi _orderApi;

        public OrderModel(IOrderApi orderRepository)
        {
            _orderApi = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderApi.GetOrderByUsername("bajinder");

            return Page();
        }       
    }
}