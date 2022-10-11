using Postieri.Data;
using Postieri.Models;
using System.Linq;

namespace Postieri.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        public OrderService(DataContext context)
        {
            _context = context;
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
    }
}
