using CleanCode.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Interface.Application
{
    public interface ICartApplication
    {
        Task<(List<Cart>, Exception)> AddCart(string userId, List<string> products);
        Task<(List<Cart>, Exception)> RemoveCart(string userId, List<string> products);
    }
}
