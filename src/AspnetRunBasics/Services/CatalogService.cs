using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client) =>
            _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var result = await _client.GetAsync("/catalog");

            return await result.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var result = await _client.GetAsync($"/catalog/GetProductByCategory/{category}");

            return await result.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var result = await _client.GetAsync($"/catalog/{id}");

            return await result.ReadContentAs<CatalogModel>();
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var response = await _client.PostAsJson($"/catalog", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CatalogModel>();
            }

            throw new Exception("Something went wrong when calling API");
        }
    }
}
