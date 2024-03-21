using Amazon.Runtime.Internal;
using CleanCode.Domain.Database.MongoInterface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Domain.Database.MongoFactory
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IConfiguration configuration;
        public MongoClientFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public MongoClient GetMongoClient() => new(configuration.GetValue<string>("Mongo:ConnectionString"));
    }
}
