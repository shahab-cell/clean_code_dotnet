using CleanCode.Domain.Database.MongoInterface;
using CleanCode.Domain.DTO;
using CleanCode.Interface.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        private readonly string? productCollection;
        public ProductRepository(IConfiguration configuration, IMongoClientFactory mongoClientFactory) : base(configuration, mongoClientFactory)
        {
            this.productCollection = configuration.GetValue<string>("Mongo:ProductCollection");
        }

        public async Task<(List<Product>, Exception)> AddProduct(Product product)
        {
            try
            {
                //-- Access Collection
                var productcollection = mongoClient.GetDatabase(this.databaseName).GetCollection<Product>(this.productCollection);
                //-- Unique Id Created
                product.Id = ObjectId.GenerateNewId().ToString();
                product.CreatedAt = DateTime.UtcNow;
                product.IsDeleted = false;
                // Convert string properties to lowercase
                _ = product.Name.ToLower();

                //-- Insert
                await productcollection.InsertOneAsync(product);

                List<Product> products = [product];
                if (products.Count > 0)
                {
                    return (products, new Exception());
                }
                return (new List<Product>(), new Exception());
            }
            catch (Exception ex) 
            {
                return (new List<Product>(), ex);
            }
        }

        public async Task<(List<Product>, Exception)> EditProduct(Product product)
        {
            //-- Break if product is null
            ArgumentNullException.ThrowIfNull(product);
            try
            {
                //-- Access Collection
                var productcollection = mongoClient.GetDatabase(this.databaseName).GetCollection<Product>(this.productCollection);
                var productFilter = Builders<Product>.Filter.Eq(p => p.IsDeleted, false)
                                    & Builders<Product>.Filter.Eq(p => p.Id, product.Id);
                product.UpdatedAt = DateTime.UtcNow;
                    
                //-- Update
                var productEdit = await productcollection.FindOneAndReplaceAsync(productFilter, product);
                List<Product> products = [productEdit];
                if (products.Count > 0)
                {
                    return (products, new Exception());
                }
                return (new List<Product>(), new Exception());
            }
            catch (Exception ex)
            {
                return (new List<Product>(), ex);
            }
        }

        public async Task<(List<Product>, Exception)> GetProductById(string productId)
        {
            ArgumentNullException.ThrowIfNull(productId);
            try
            {
                //-- Access Collection
                var productcollection = mongoClient.GetDatabase(this.databaseName).GetCollection<Product>(this.productCollection);
                var productFilter = Builders<Product>.Filter.Eq(p => p.IsDeleted, false)
                                    & Builders<Product>.Filter.Eq(p => p.Id, productId);

                //-- Find Signle Record
                var product = await productcollection.Find(productFilter).FirstOrDefaultAsync();
                List<Product> products = [product];
                if (products.Count > 0)
                {
                    return (products, new Exception());
                }
                return (new List<Product>(), new Exception());
            }
            catch (Exception ex)
            {
                return (new List<Product>(), ex);
            }
        }

        public Task<(List<Product>, Exception)> GetProducts(string typeCategory, string animeCategory, int minPrice, int maxPrice)
        {
            throw new NotImplementedException();
        }
    }
}
