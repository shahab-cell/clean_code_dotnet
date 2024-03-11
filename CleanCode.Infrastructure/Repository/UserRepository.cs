using CleanCode.Domain.DTO;
using CleanCode.Interface.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() 
        {
        }

        public async Task<User> GetAllUser()
        {
            return new User();
        }
    }
}
