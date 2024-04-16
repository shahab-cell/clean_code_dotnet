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
    public class CartRepository : RepositoryBase, ICartRepository
    {
        private readonly string? cartCollection;
        public CartRepository(IConfiguration configuration, IMongoClientFactory mongoClientFactory) : base(configuration, mongoClientFactory)
        {
            this.cartCollection = configuration.GetValue<string>("Mongo:CartCollection");
        }

        public async Task<(List<Cart>, Exception)> AddCart(string userId, List<string> products)
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

        public async Task<(List<Cart>, Exception)> RemoveCart(string userId, List<string> products)
        {
            try
            {
                //-- Access Collection
                var cartcollection = mongoClient.GetDatabase(this.databaseName).GetCollection<Cart>(this.cartCollection);
                //-- Get User's Cart
                var cartFilter = Builders<Cart>.Filter.Eq(c => c.UserId, userId)
                                 & Builders<Cart>.Filter.Eq(c => c.UserId, userId);

                var cart = await cartcollection.Find(cartFilter).FirstOrDefaultAsync();

                List<string> matchIds = new();
                foreach (var productId in cart.ProductIds) 
                {
                    var matchId = products.Contains(productId);
                    if (matchId == true)
                    {
                        matchIds.Add(productId);
                    }
                }

                //-- Update Cart
                if (matchIds.Count > 0)
                {
                    cart.ProductIds.RemoveAll(productId => matchIds.Contains(productId));
                }

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
