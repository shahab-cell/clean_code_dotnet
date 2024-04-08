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
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository productRepository;
        public ProductApplication(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }

        public async Task<(List<Product>, Exception)> AddProduct(Product product)
        {
            return await productRepository.AddProduct(product);
        }

        public async Task<(List<Product>, Exception)> EditProduct(Product product)
        {
            return await productRepository.EditProduct(product);
        }

        public async Task<(List<Product>, Exception)> GetProductById(string productId)
        {
            return await productRepository.GetProductById(productId);
        }

        public async Task<(List<Product>, Exception)> GetProducts(string typeCategory, string animeCategory, int minPrice, int maxPrice)
        {
            return await productRepository.GetProducts(typeCategory, animeCategory, minPrice, maxPrice);
        }
    }
}
