﻿using Amazon.Runtime.Internal;
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
    public class RepositoryBase
    {
        private readonly IConfiguration configuration;
        private readonly string? databaseName;
        private readonly MongoClient mongoClient;
        protected RepositoryBase(IConfiguration configuration, IMongoClientFactory mongoClientFactory) 
        {
            this.configuration = configuration;
            this.databaseName = configuration.GetConnectionString("Mongo:DatabaseName");
            this.mongoClient = mongoClientFactory.GetMongoClient();
        }
    }
}
