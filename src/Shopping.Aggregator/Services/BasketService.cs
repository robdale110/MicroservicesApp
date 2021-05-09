﻿using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client) =>
            _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<BasketModel> GetBasket(string userName)
        {
            var result = await _client.GetAsync($"/api/v1/basket/{userName}");

            return await result.ReadContentAs<BasketModel>();
        }
    }
}
