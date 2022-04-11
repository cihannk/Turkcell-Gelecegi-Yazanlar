using bootShop.Dtos.Requests;
using bootShop.Dtos.Responses;
using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductsResponse>> GetProducts();
        Task<int> AddProduct(AddProductRequest product);
        Task<int> UpdateProduct(UpdateProductRequest product);
        Task<bool> IsExist(int id);
        Task<GetProductsResponse> GetProductById(int id);
        Task SoftDeleteProduct(int id);
    }
}
