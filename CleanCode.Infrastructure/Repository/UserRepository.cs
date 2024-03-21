﻿using CleanCode.Domain.Database.MongoFactory;
using CleanCode.Domain.Database.MongoInterface;
using CleanCode.Domain.DTO;
using CleanCode.Interface.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly string? userCollection;
        public UserRepository(IConfiguration configuration, IMongoClientFactory mongoClientFactory) : base(configuration, mongoClientFactory)
        {
            this.userCollection = configuration.GetValue<string>("Mongo:UserCollection");
        }

        public async Task<(List<User>, Exception)> PostUser(User user)
        {
            try
            {
                //-- Access Collection
                var usercollection = mongoClient.GetDatabase(this.databaseName).GetCollection<User>(this.userCollection);
                var userFilter = Builders<User>.Filter.Empty;

                // Convert string properties to lowercase
                PropertyInfo[] properties = typeof(User).GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        if (property.GetValue(user) is string value)
                        {
                            property.SetValue(user, value.ToLower());
                        }
                    }
                }

                //-- Unique Id Created
                user.UserId = ObjectId.GenerateNewId().ToString();

                //-- Insert
                await usercollection.InsertOneAsync(user);
                
                List<User> users = [user];
                if (users.Count > 0)
                {
                    return (users, new Exception());
                }
                return (new List<User>(), new Exception());
            }
            catch (Exception ex)
            {
                return (new List<User>(), ex);
            }
        }

        public async Task<(List<User>, Exception)> GetAllUser()
        {
            try
            {
                var usercollection = mongoClient.GetDatabase(this.databaseName).GetCollection<User>(this.userCollection);
                var userFilter = Builders<User>.Filter.Empty;
                var users = await usercollection.Find(userFilter).ToListAsync();
                if (users.Count > 0)
                {
                    return (users, new Exception());
                }
                return (new List<User>(), new Exception());
            }
            catch (Exception ex)
            {
                return (new List<User>(), ex);
            }
        }
    }
}
