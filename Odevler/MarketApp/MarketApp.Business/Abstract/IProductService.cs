using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IProductService
    {
        Task<int> AddProduct(AddProductRequest product);
        Task RemoveProduct(int id);
        Task<int> UpdateProduct(UpdateProductRequest product);
        Task<IList<GetProductsResponse>> GetProducts();
        Task<IList<GetProductsResponse>> GetProductsWithoutCheckingActive();
        Task<IList<GetProductsResponse>> GetProductsByCategoryName(string categoryName);
        Task<GetProductsResponse> GetProduct(int id);
        Task<GetProductsResponse> GetProductWithoutCheckingActive(int id);
    }
}
