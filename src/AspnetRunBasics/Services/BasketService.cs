using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client) =>
            _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/basket/{userName}");

            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var response = await _client.PostAsJson($"/basket", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<BasketModel>();
            }

            throw new Exception("Something went wrong when calling the API");
        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            var response = await _client.PostAsJson($"/basket/checkout", model);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when calling the API");
            }
        }
    }
}
