using bootShop.Dtos.Requests;
using bootShop.Dtos.Responses;
using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public class FakeProductService : IProductService
    {
        private List<Product> products;
        public FakeProductService()
        {
            products = new List<Product> {
                new Product { Id= 1, CategoryId=1, Name= "IPhone", Price=1200.0, Discount= 0.1,ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 2, CategoryId=1, Name= "Samsung", Price = 1200.0, Discount= 0.1, ImageUrl = "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 3,CategoryId=1, Name= "Xiaomi", Price=1200.0, Discount= 0.1, ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 4,CategoryId=1, Name= "Huawei", Price=1200.0, Discount= 0.1, ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 5,CategoryId=2, Name= "Macbook", Price=1200.0, Discount= 0.1, ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 6,CategoryId=2, Name= "MSI", Price=1200.0, Discount= 0.1, ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 7,CategoryId=3, Name= "Xbox", Price=1200.0, Discount= 0.1, ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"},
                new Product { Id= 8,CategoryId=3, Name= "Ps", Price=1200.0, Discount= 0.1, ImageUrl= "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp"}};
        }

        public Task<int> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddProduct(AddProductRequest product)
        {
            throw new NotImplementedException();
        }

        public Task<GetProductsResponse> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProduct(UpdateProductRequest product)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<GetProductsResponse>> IProductService.GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
