using CleanCode.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Interface.Application
{
    public interface IProductApplication
    {
        Task<(List<Product>, Exception)> AddProduct(Product product);
        Task<(List<Product>, Exception)> EditProduct(Product product);
        Task<(List<Product>, Exception)> GetProductById(string productId);
        Task<(List<Product>, Exception)> GetProducts(string typeCategory, string animeCategory, int minPrice, int maxPrice);
    }
}
