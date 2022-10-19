using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;
using System.Linq;
using System.Security.Claims;

namespace Postieri.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }
        public bool AddOrder(Order request)
        {
            var order = new Order()
            {
                OrderId = request.OrderId,
                //ProductId = request.ProductId,
                Date = request.Date,
                OrderedOn = request.OrderedOn,
                Price = request.Price,
                UserId = request.UserId,
                CompanyId = request.CompanyId,
                Address = request.Address,
                Sign = request.Sign,
                Status = request.Status,
                CourierId = request.CourierId,
                ManagerId = request.ManagerId
            };
            if (order == null)
            {
                return false;
            }
            else if (OrderExists(order))
            {
                return false;
            }
            else
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateOrder(Order request)
        {
            var order = _context.Orders.Find(request.OrderId);
            if (order == null)
            {
                return false;
            }
            else if (!OrderExists(order))
            {
                return false;
            }
            else
            {
                order.OrderId = request.OrderId;
                order.Date = request.Date;
                order.OrderedOn = request.OrderedOn;
                order.Price = request.Price;
                order.UserId = request.UserId;
                order.CompanyId = request.CompanyId;
                order.Address = request.Address;
                order.Sign = request.Sign;
                order.Status = request.Status;
                order.CourierId = request.CourierId;
                order.ManagerId = request.ManagerId;

                _context.SaveChanges();
                return true;
            }
        }
        public bool DeleteOrder(Guid OrderId)
        {
            var order = _context.Orders.Find(OrderId);
            if (order == null)
            {
                return false;
            }
            else if (!OrderExists(order))
            {
                return false;
            }
            else
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return true;
            }
        }

        public List<Order> GetOrderById(Guid OrderId)
        {
            var order = _context.Orders.Where(n => n.OrderId == OrderId).ToList();
            return order;
        }
        public bool OrderExists(Order request)
        {
            bool alreadyExist = _context.Orders.Any(x => x.OrderId == request.OrderId);
            return alreadyExist;
        }
        private string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
        private string GetRole()
        {
            var role = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return role;
        }
        public List<Order> GetOrdersByRole()
        {
            var result = GetMyName();
            var role = GetRole();
          
            if (role == "Courier")
            {
                var courier = _context.Users.Where(x => x.Email == result).FirstOrDefault();
                var orders = _context.Orders.Where(x => x.CourierId == courier.UserId).Include(x => x.Products).ToList();
                return orders;
            }
            else if (role == "Manager" || role == "Administrator")
            {
                return _context.Orders.Include(x => x.Products).ToList();
            }
            else if(role == "Storekeeper")
            {
                return _context.Orders.Where(x => x.Status == "accept").Include(x => x.Products).ToList();
            }
            var user = _context.Users.Where(x => x.Email == result).FirstOrDefault();
            return _context.Orders.Where(x => x.UserId == user.UserId).Include(x => x.Products).ToList();
        }
    }
}
