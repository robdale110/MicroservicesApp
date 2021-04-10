using Catalog.Api.Data.Interfaces;
using Catalog.Api.Entities;
using Catalog.Api.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Product>> GetProducts() =>
            await _context.Products
                .Find(p => true)
                .ToListAsync();

        public async Task<Product> GetProduct(string id) =>
            await _context.Products
                .Find(p => p.Id == id)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductByName(string name) =>
            await _context.Products
                .Find(Builders<Product>.Filter.ElemMatch(p => p.Name, name))
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName) =>
            await _context.Products
                .Find(Builders<Product>.Filter.Eq(p => p.Category, categoryName))
                .ToListAsync();

        public async Task Create(Product product) =>
            await _context.Products
                .InsertOneAsync(product);

        public async Task<bool> Update(Product product)
        {
            var updateResult =
                await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var deleteResult = await _context.Products
                .DeleteOneAsync(Builders<Product>.Filter.Eq(p => p.Id, id));

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
