using Amazon.Runtime.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repository
{
    public class RepositoryBase
    {
        protected RepositoryBase(IConfiguration configuration) 
        {

        }
    }
}
