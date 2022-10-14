using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Postieri.Data;
using Postieri.DTOs;
using Postieri.Models;
using System.Security.Principal;

namespace Postieri.Services
{
    public class CourierService : ICourierService
    {
        private readonly DataContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;

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
        public List<Order> AcceptOrder(Guid order, Guid courierId)
        {
            var orderId = _dbcontext.Orders.Find(order);

            if(orderId != null && orderId.CourierId == courierId)
            {
                orderId.Status = "accepted";
                _dbcontext.SaveChanges();
            }     
            var CourierUserId = _dbcontext.Users.Find(courierId);

            return _dbcontext.Orders.Where(o => o.CourierId == CourierUserId.UserId).ToList();
            
        }
        public void DeclineOrder(Guid orderId)
        {
            var order = _dbcontext.Orders.Find(orderId);

            if (order != null)
            {
                order.Status = "declined";
                order.CourierId = null;
                _dbcontext.SaveChanges();
            }
        }
    }
}
