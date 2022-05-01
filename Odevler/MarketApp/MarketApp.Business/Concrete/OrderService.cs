﻿using MarketApp.Business.Abstract;
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

        public OrderService(IUserRepository userRepository,IAddressRepository addressRepository, IOrderRepository orderRepository, ICartItemRepository cartItemRepository )
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
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
                    var existCartItem = await _cartItemRepository.GetExistCartItem(order.CartItems[i].ProductId, order.CartItems[i].Amount);
                    if (existCartItem != null)
                    {
                        order.CartItems[i] = existCartItem;
                    }
                    else
                    {
                        await _cartItemRepository.Add(order.CartItems[i]);
                    }
                }
                await _orderRepository.Add(order);
            }
            else
            {
                throw new Exception("Order couldn't proceed");
            }
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