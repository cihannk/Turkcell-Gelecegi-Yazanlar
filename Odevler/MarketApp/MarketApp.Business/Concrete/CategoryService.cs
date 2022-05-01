using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.DataAccess.Repositories;
using MarketApp.Dtos.Request;
using MarketApp.Dtos.Response;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> AddCategory(AddCategoryRequest category)
        {
            var cat = await _repository.GetByName(category.Name);
            if ( cat != null)
            {
                throw new InvalidOperationException("Category is already exist with given name");
            }
            var entity = _mapper.Map<Category>(category);
            return await _repository.Add(entity);
        }

        public async Task<IList<GetCategoriesResponse>> GetCategories()
        {
            var entities = await _repository.GetAllEntities();
            if (entities == null)
            {
                throw new InvalidOperationException("There is no category found");
            }
            return _mapper.Map<IList<GetCategoriesResponse>>(entities);
        }

        public async Task<GetCategoriesResponse> GetCategory(int id)
        {
            if (await _repository.IsExist(id))
            {
                var entity = await _repository.GetEntityById(id);
                return _mapper.Map<GetCategoriesResponse>(entity);
            }
            throw new InvalidOperationException("Category couldn't found");
        }

        public async Task<Category> GetCategoryEntity(int id)
        {
            if (await _repository.IsExist(id))
            {
                return await _repository.GetEntityById(id);
            }
            throw new InvalidOperationException("Category is not exist");
        }

        public async Task RemoveCategory(int id)
        {
            if (await _repository.IsExist(id))
            {
                await _repository.Delete(id);
            }
            else
            {
                throw new InvalidOperationException("Category couldn't found");
            }
        }

        public async Task<int> UpdateCategory(UpdateCategoryRequest category)
        {
            var entity = _mapper.Map<Category>(category);
            return await _repository.Update(entity);
        }
    }
}
