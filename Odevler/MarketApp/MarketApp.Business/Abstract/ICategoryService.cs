using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface ICategoryService
    {
        Task<int> AddCategory(AddCategoryRequest category);
        Task RemoveCategory(int id);
        Task<int> UpdateCategory(UpdateCategoryRequest category);
        Task<IList<GetCategoriesResponse>> GetCategories();
        Task<GetCategoriesResponse> GetCategory(int id);
        Task<Category> GetCategoryEntity(int id);
    }
}
