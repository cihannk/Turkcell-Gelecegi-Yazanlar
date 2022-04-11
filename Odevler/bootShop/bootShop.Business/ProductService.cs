using AutoMapper;
using bootShop.DataAccess.Repositories;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<int> AddProduct(AddProductRequest product)
        {
            var productEntity = _mapper.Map<Product>(product);
            var result = await _productRepository.Add(productEntity);
            return result;
        }

        public async Task<GetProductsResponse> GetProductById(int id)
        {
            var productEntity = await _productRepository.GetEntityById(id);

            var product = _mapper.Map<GetProductsResponse>(productEntity);
            return product;
        }

        public async Task<IEnumerable<GetProductsResponse>> GetProducts()
        {
            var productEntities = await _productRepository.GetAllEntities();

            productEntities = productEntities.Where(x => x.IsDeleted == false);
            var products = _mapper.Map<IEnumerable<GetProductsResponse>>(productEntities);

            return products;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _productRepository.IsExist(id);
        }

        public async Task SoftDeleteProduct(int id)
        {
            await _productRepository.SoftDelete(id);
        }

        public async Task<int> UpdateProduct(UpdateProductRequest product)
        {
            var productEntity = _mapper.Map<Product>(product);
            productEntity.ModifiedDate = DateTime.Now;
            var result = await _productRepository.Update(productEntity);
            return result;
        }
    }
}
