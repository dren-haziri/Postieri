﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Postieri.Data;
using Postieri.DTOs;
using Postieri.Models;

namespace Postieri.Services
{
    public class CourierService : ICourierService
    {
        private readonly DataContext _dbcontext;

        public CourierService(DataContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void UpdateStatus(Guid orderId, string status)
        {
            var order = _dbcontext.Orders.Find(orderId);
            if(order != null)
            {
                 order.Status = status;
                _dbcontext.SaveChanges();
            }
        } 
        public List<Order> GetOrdersForCourier(Guid courierId)
        {
            var CourierUserId = _dbcontext.Users.Find(courierId);
            return _dbcontext.Orders.Where(o => o.CourierId == CourierUserId.UserId).ToList();
        }  
        public bool AcceptOrder(Guid order, Guid courierId)
        {
            var orderId = _dbcontext.Orders.Find(order);

            if(orderId != null && orderId.CourierId == courierId)
            {
                orderId.Status = "accepted";
                _dbcontext.SaveChanges();
                return true;
            }
            else
            {
                return false;   
            }              
        }
        public bool DeclineOrder(Guid orderId, Guid courierId)
        {
            var order = _dbcontext.Orders.Find(orderId);

            if (order != null && order.CourierId == courierId)
            {
                order.Status = "declined";
                order.CourierId = null;
                _dbcontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
