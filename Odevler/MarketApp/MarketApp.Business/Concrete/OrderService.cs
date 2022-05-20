using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.Business.Constants.ErrorMessages;
using MarketApp.DataAccess.Repositories;
using MarketApp.DataAccess.Repositories.Abstract;
using MarketApp.Dtos.Request;
using MarketApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IUserRepository userRepository,IAddressRepository addressRepository, IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IProductRepository productRepository, IMapper mapper )
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task BeginOrder(AddOrderRequest order)
        {
            var isUserExist = await _userRepository.IsExist(order.UserId);
            var isAddressExist = await _addressRepository.IsExist(order.AddressId);

            if (isUserExist && isAddressExist && order.CartItems != null)
            {
                // for creation of cartItems
                for(int i = 0; i<order.CartItems.Count; i++)
                {
                    order.CartItems[i] = await CreateCartItem(order.CartItems[i]);
                }
                var orderEntity = _mapper.Map<Order>(order);
                orderEntity.OrderDate = DateTime.Now;
                await _orderRepository.Add(orderEntity);
            }
            else
            {
                throw new Exception(ErrorMessages.Order.NotProceed);
            }
        }

        private async Task<AddCartItemRequest> CreateCartItem(AddCartItemRequest addCartItemRequest)
        {
                var cartItem = new CartItem {
                    ProductId = addCartItemRequest.ProductId,
                    Amount = addCartItemRequest.Amount,
                    PastPrice= addCartItemRequest.PastPrice
                };
                await _cartItemRepository.Add(cartItem);
            return addCartItemRequest;
        }

        public async Task DeleteOrder(int id)
        {
            if (await _orderRepository.IsExist(id)){
                await _orderRepository.Delete(id);
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.Order.NotFoundWithGivenOrderId);
            }
        }

        public async Task<IList<Order>> GetAllOrders()
        {
            var allOrders = await _orderRepository.GetAllEntities();
            if (allOrders != null)
            {
                return allOrders;
            }
            throw new InvalidOperationException(ErrorMessages.Order.NoOrder);
        }

        public async Task<Order> GetOrderById(int id)
        {
            if (await _orderRepository.IsExist(id))
            {
                var order = await _orderRepository.GetEntityById(id);
                return order;
            }
            throw new InvalidOperationException(ErrorMessages.Order.NotFoundWithGivenOrderId);
        }

        public async Task<IList<Order>> GetOrdersByUserId(int userId)
        {
            var orders =  await _orderRepository.GetEntitesByUserId(userId);
            if (orders != null) {
                return orders;
            }
            throw new InvalidOperationException(ErrorMessages.Order.NotFoundWithGivenUserId);
        }

        public async Task UpdateOrder(UpdateOrderRequest order)
        {
            if (await _orderRepository.IsExist(order.Id))
            {
                if (await _userRepository.IsExist(order.UserId))
                {
                    var orderEntity = _mapper.Map<Order>(order);
                    await _orderRepository.Update(orderEntity);
                }
                else
                {
                    throw new InvalidOperationException(ErrorMessages.User.NotFoundWithGivenUserId);
                }
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.Order.NotFoundWithGivenOrderId);
            }

        }
        public async Task ClearAllCartItemsInOrder(int orderId)
        {
            if(await _orderRepository.IsExist(orderId))
            {
                await _orderRepository.ClearAllCartItems(orderId);
            }
            else
            {
                throw new InvalidOperationException(ErrorMessages.Order.NotFoundWithGivenOrderId);
            }
        }
    }
}