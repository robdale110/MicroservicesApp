using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client) =>
            _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var result = await _client.GetAsync("/api/v1/catalog");

            return await result.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var result = await _client.GetAsync($"/api/v1/catalog/GetProductByCategory/{category}");

            return await result.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var result = await _client.GetAsync($"/api/v1/catalog/{id}");

            return await result.ReadContentAs<CatalogModel>();
        }
    }
}
