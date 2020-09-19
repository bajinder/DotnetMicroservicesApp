using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class BasketCart
    {
        public string Username { get; set; }
        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

        public BasketCart()
        {

        }
        public BasketCart(string username)
        {
            Username = username;
        }
        public decimal TotalPrice { get 
            {
                decimal totalPrice = 0;
                foreach(var item in Items)
                {
                    totalPrice += (item.Quantity * item.Price);
                }
                return totalPrice;
            }  
        }

    }
}
