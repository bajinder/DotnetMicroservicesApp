using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Store.ApiCollection.Infrastructure
{
    public class BaseHttpClientWithFactory
    {
        private IHttpClientFactory _factory;

        public BaseHttpClientWithFactory(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public virtual async Task<T> SendRequest<T>(HttpRequestMessage request) where T : class
        {
            var client = GetHttpClient();
            var response = await client.SendAsync(request);
            T result = null;

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<T>(GetFormatters());
            return result;
        }

        protected virtual IEnumerable<MediaTypeFormatter> GetFormatters()
        {
            return new List<MediaTypeFormatter> { new JsonMediaTypeFormatter() };
        }

        private HttpClient GetHttpClient()
        {
            return _factory.CreateClient();
        }
    }
}
