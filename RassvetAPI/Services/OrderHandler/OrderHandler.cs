﻿using RassvetAPI.Models.RassvetDBModels;
using RassvetAPI.Models.RequestModels;
using System;
using System.Threading.Tasks;

namespace RassvetAPI.Services.OrderHandler
{
    public class OrderHandler : IOrderHandler
    {
        private readonly RassvetDBContext _dao;

        public OrderHandler(RassvetDBContext dao)
        {
            _dao = dao;
        }

        public async Task MakeOrderAsync(int offerId, int clientId)
        {
            var offer = await _dao.Offers.FindAsync(offerId);

            if (offer is null)
                throw new OfferNotFoundException("Такой услуги не существует.");

            await _dao.SubscriptionOrders.AddAsync(new SubscriptionOrder
            {
                OfferId = offerId,
                ClientId = clientId,
                Date = DateTime.Now,
                Confirmed = false
            });

            await _dao.SaveChangesAsync();
        }

        public async Task ProcessOrderAsync(int orderId, int managerId)
        {
            var order = await _dao.SubscriptionOrders.FindAsync(orderId);

            if (order is null)
                throw new OrderNotFoundException("Такого заказа не существует.");

            order.Confirmed = true;

            //var bill = new Bill
            //{
            //    ManagerId = managerId,
            //    Price = order.Offer.Price,
            //    OrderId = orderId
            //};

            //_dao.Bills.Add(bill);

            await _dao.SaveChangesAsync();
        }
    }

    public abstract class OrderException : Exception
    {
        public OrderException() : base() { }

        public OrderException(string message) : base(message) { }
    }

    public class OfferNotFoundException : OrderException 
    {
        public OfferNotFoundException() : base() { }

        public OfferNotFoundException(string message) : base(message) { }
    }

    public class OrderNotFoundException : OrderException
    {
        public OrderNotFoundException() : base() { }

        public OrderNotFoundException(string message) : base(message) { }
    }
}
