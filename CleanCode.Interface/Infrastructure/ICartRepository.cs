using CleanCode.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Interface.Infrastructure
{
    public interface ICartRepository
    {
        Task<(List<Cart>, Exception)> AddCart(string userId, List<string> products);
        Task<(List<Cart>, Exception)> RemoveCart(string userId, List<string> products);
    }
}
