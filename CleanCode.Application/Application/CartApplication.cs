using CleanCode.Domain.DTO;
using CleanCode.Interface.Application;
using CleanCode.Interface.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.Application
{
    public class CartApplication : ICartApplication
    {
        private readonly ICartRepository cartRepository;
        public CartApplication(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<(List<Cart>, Exception)> AddCart(string userId, List<string> products)
        {
            return await cartRepository.AddCart(userId, products);
        }

        public async Task<(List<Cart>, Exception)> RemoveCart(string userId, List<string> products)
        {
            return await cartRepository.RemoveCart(userId, products);
        }
    }
}
