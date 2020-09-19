using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private IBasketContext _basketContext;

        public BasketRepository(IBasketContext basketContext)
        {
            _basketContext = basketContext;
        }
        public async Task<bool> DeleteBasket(string userName)
        {
            return await _basketContext.Redis.KeyDeleteAsync(userName);
        }

        public async Task<BasketCart> GetBasket(string userName)
        {
            var basket = await _basketContext.Redis.StringGetAsync(userName);

            if (basket.IsNullOrEmpty)
                return null;
            else
                return JsonConvert.DeserializeObject<BasketCart>(basket);            
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            var updated = await _basketContext.Redis.StringSetAsync(basket.Username, JsonConvert.SerializeObject(basket));
            if (updated)
                return await GetBasket(basket.Username);
            else
                return null;
        }
    }
}
