using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        List<Order> GetOrderById(Guid OrderId);
        bool AddOrder(Order request);
        bool UpdateOrder(Order request);
        bool DeleteOrder(Order OrderId);
    }
}
