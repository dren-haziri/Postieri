using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Postieri.Data;
using Postieri.DTOs;
using Postieri.Models;
using System.Security.Claims;

namespace Postieri.Services
{
    public class CourierService : ICourierService
    {
        private readonly DataContext _dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourierService(DataContext dbcontext, IHttpContextAccessor httpContextAccessor)
        {
            _dbcontext = dbcontext;
            _httpContextAccessor = httpContextAccessor;
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

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        public List<Order> GetOrdersForCourier()
        {
            var result = GetMyName();
            var courier = _dbcontext.Users.Where(x => x.Email == result).FirstOrDefault();
            var orders = _dbcontext.Orders.Where(x => x.CourierId == courier.UserId).Include(x => x.Products).ToList();
            return orders;
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
