using Store.ApiCollection.Infrastructure;
using Store.ApiCollection.Interfaces;
using Store.Models;
using Store.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiCollection
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderApi
    {
        private readonly IApiSettings _settings;

        public OrderApi(IHttpClientFactory factory,IApiSettings settings) : base(factory)
        {
            _settings = settings;
        }
        public async Task<IEnumerable<OrderResponseModel>> GetOrderByUsername(string username)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.OrderPath)
                               .AddQueryString("username", username)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponseModel>>(message);
        }
    }
}
