using AutoMapper;
using MarketApp.Business.Abstract;
using MarketApp.DataAccess.Repositories;
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
        public async Task BeginOrder(Order order)
        {
            var user = await _userRepository.GetEntityById(order.UserId);
            var address = await _addressRepository.GetEntityById(order.AddressId);

            if (user != null && address != null && order.CartItems != null)
            {
                // for creation of cartItems
                for(int i = 0; i<order.CartItems.Count; i++)
                {
                    order.CartItems[i] = await CreateCartItem(order.CartItems[i].ProductId, order.CartItems[i].Amount, order.CartItems[i].PastPrice);
                }
                await _orderRepository.Add(order);
            }
            else
            {
                throw new Exception("Order couldn't proceed");
            }
        }

        public async Task<CartItem> CreateCartItem(int productId, int amount, double pastPrice)
        {
                var product = await _productRepository.GetEntityById(productId);
                var cartItem = new CartItem {
                    ProductId = productId,
                    Amount = amount,
                    PastPrice= pastPrice
                };
                await _cartItemRepository.Add(cartItem);
            return cartItem;
        }

        public async Task DeleteOrder(int id)
        {
            if (await _orderRepository.IsExist(id)){
                await _orderRepository.Delete(id);
            }
            else
            {
                throw new InvalidOperationException("Sipariş veritabanında yok");
            }
        }

        public async Task<IList<Order>> GetAllOrders()
        {
            var allOrders = await _orderRepository.GetAllEntities();
            if (allOrders != null)
            {
                return allOrders;
            }
            throw new InvalidOperationException("Veritabanında sipariş yok");
        }

        public async Task<Order> GetOrderById(int id)
        {
            if (await _orderRepository.IsExist(id))
            {
                var order = await _orderRepository.GetEntityById(id);
                return order;
            }
            throw new InvalidOperationException("Veritabanında sipariş bulunamadı");
        }

        public async Task<IList<Order>> GetOrdersByUserId(int userId)
        {
            var orders =  await _orderRepository.GetEntitesByUserId(userId);
            if (orders != null) {
                return orders;
            }
            throw new InvalidOperationException("There is no order with specified userId");
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
                    throw new InvalidOperationException("Kullanıcı veritanabında yok");
                }
            }
            else
            {
                throw new InvalidOperationException("Order veritabanında yok");
            }

        }
    }
}