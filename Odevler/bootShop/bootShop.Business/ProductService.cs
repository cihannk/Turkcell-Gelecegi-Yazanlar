using bootShop.DataAccess.Repositories;
using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddProduct(Product product)
        {
            var result = await _productRepository.Add(product);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productRepository.GetAllEntities();
            return products;
        }
    }
}
