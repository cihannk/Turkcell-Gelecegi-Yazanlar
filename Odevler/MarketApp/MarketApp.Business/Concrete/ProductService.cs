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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<int> AddProduct(AddProductRequest product)
        {
            if (await _productRepository.GetByName(product.Name) != null)
            {
                throw new InvalidOperationException("Product with given name is already exist");
            }
            var productToAdd = _mapper.Map<Product>(product);
            return await _productRepository.Add(productToAdd);
        }

        public async Task<GetProductsResponse> GetProduct(int id)
        {
            if (await _productRepository.IsExist(id))
            {
                var productEntity = await _productRepository.GetEntityById(id);
                if (productEntity.IsActive)
                {
                    return _mapper.Map<GetProductsResponse>(productEntity);
                }
            }
            throw new InvalidOperationException("Product couldn't found");
        }

        public async Task<GetProductsResponse> GetProductWithoutCheckingActive(int id)
        {
            if (await _productRepository.IsExist(id))
            {
                var productEntity = await _productRepository.GetEntityById(id);
                return _mapper.Map<GetProductsResponse>(productEntity);
            }
            throw new InvalidOperationException("Product couldn't found");
        }

        public async Task<IList<GetProductsResponse>> GetProducts()
        {

            var entities =  await _productRepository.GetAllEntities();
            // filtering IsActive
            var filtered = getActiveEntities(entities);

            if (filtered == null)
            {
                throw new InvalidOperationException("There is no product");
            }
            
            return _mapper.Map<IList<GetProductsResponse>>(filtered);
        }
        public async Task<IList<GetProductsResponse>> GetProductsWithoutCheckingActive()
        {

            var entities = await _productRepository.GetAllEntities();

            if (entities == null)
            {
                throw new InvalidOperationException("There is no product");
            }

            return _mapper.Map<IList<GetProductsResponse>>(entities);
        }

        private IList<Product> getActiveEntities(IList<Product> entities )
        {
            return entities.Where(x => x.IsActive).ToList();
        }

        public async Task<IList<GetProductsResponse>> GetProductsByCategoryName(string categoryName)
        {
            var category = await _categoryRepository.GetByName(categoryName);
            if (category == null)
            {
                throw new InvalidOperationException("Category is not exist with given categoryName");
            }
            
            var products = _mapper.Map<IList<GetProductsResponse>>(getActiveEntities(category.Products));
            if (products == null)
            {
                throw new InvalidOperationException("Products are not exist with given categoryName");
            }
            return products;
        }

        public async Task RemoveProduct(int id)
        {
            if (await _productRepository.IsExist(id))
                await _productRepository.Delete(id);
            else
                throw new InvalidOperationException("Product is not exist");
        }

        public async Task<int> UpdateProduct(UpdateProductRequest product)
        {
            if (!await _productRepository.IsExist(product.Id))
            {
                throw new InvalidOperationException("Product cannot be updated because is not exist");
            }
            var dbEntity = await _productRepository.GetEntityById(product.Id);
            var entity = _mapper.Map<Product>(product);

            entity.CreatedDate = dbEntity.CreatedDate;
            entity.ModifiedDate = DateTime.Now;

            return await _productRepository.Update(entity);
        }
    }
}
