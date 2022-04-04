using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public class FakeCategoryService : ICategoryService
    {
        private List<Category> _categories;
        public FakeCategoryService()
        {
            _categories = new List<Category> {
                new Category { Id = 1, Name = "Telefonlar"},
                new Category { Id = 2, Name = "Bilgisayarlar"},
                new Category { Id = 3, Name = "Konsollar"},
            };
        }
        public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }

        Task<IEnumerable<Category>> ICategoryService.GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
