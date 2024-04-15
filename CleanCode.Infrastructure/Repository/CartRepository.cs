using CleanCode.Domain.Database.MongoInterface;
using CleanCode.Domain.DTO;
using CleanCode.Interface.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repository
{
    public class CartRepository : RepositoryBase, ICartRepository
    {
        private readonly string? cartCollection;
        public CartRepository(IConfiguration configuration, IMongoClientFactory mongoClientFactory) : base(configuration, mongoClientFactory)
        {
            this.cartCollection = configuration.GetValue<string>("Mongo:CartCollection");
        }

        public async Task<(List<Cart>, Exception)> AddCart(string userId, List<Product> products)
        {
            try
            {
                //-- Access Collection
                var cartcollection = mongoClient.GetDatabase(this.databaseName).GetCollection<Cart>(this.cartCollection);
                //-- Create Cart
                Cart cart = new Cart
                {
                    UserId = userId,
                    ProductIds = products,
                    CartId = ObjectId.GenerateNewId().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                //-- Insert
                await cartcollection.InsertOneAsync(cart);

                List<Cart> carts = [cart];
                if (carts.Count > 0)
                {
                    return (carts, new Exception());
                }
                return (new List<Cart>(), new Exception());
            }
            catch (Exception ex)
            {
                return (new List<Cart>(), ex);
            }
        }
    }
}
