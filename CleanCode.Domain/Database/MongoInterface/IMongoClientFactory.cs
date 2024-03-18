using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Domain.Database.MongoInterface
{
    public interface IMongoClientFactory
    {
        public MongoClient GetMongoClient();
    }
}
