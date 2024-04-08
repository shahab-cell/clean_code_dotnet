using Amazon.Runtime.Internal;
using CleanCode.Domain.Database.MongoInterface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repository
{
    public abstract class RepositoryBase
    {
        protected readonly IConfiguration configuration;
        protected readonly string? databaseName;
        protected readonly MongoClient mongoClient;
        protected RepositoryBase(IConfiguration configuration, IMongoClientFactory mongoClientFactory) 
        {
            this.configuration = configuration;
            this.databaseName = configuration.GetValue<string>("Mongo:DatabaseName");
            this.mongoClient = mongoClientFactory.GetMongoClient();
        }
    }
}
