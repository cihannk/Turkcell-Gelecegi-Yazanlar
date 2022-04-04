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

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        Task<IEnumerable<Product>> IProductService.GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
