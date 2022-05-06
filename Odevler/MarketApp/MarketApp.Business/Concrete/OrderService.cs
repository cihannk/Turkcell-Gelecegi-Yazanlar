using MarketApp.Business.Abstract;
using MarketApp.DataAccess.Repositories;
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

        public OrderService(IUserRepository userRepository,IAddressRepository addressRepository, IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IProductRepository productRepository )
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
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

        public async Task<IList<Order>> GetAllOrders()
        {
            var allOrders = await _orderRepository.GetAllEntities();
            if (allOrders != null)
            {
                return allOrders;
            }
            throw new InvalidOperationException("There is no order in db");
        }

        public async Task<IList<Order>> GetOrdersByUserId(int userId)
        {
            var orders =  await _orderRepository.GetEntitesByUserId(userId);
            if (orders != null) {
                return orders;
            }
            throw new InvalidOperationException("There is no order with specified userId");
        }
    }
}
