using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class BasketModel
    {
        public string Username { get; set; }
        public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();

        public decimal TotalPrice { get; set; }

    }
}
