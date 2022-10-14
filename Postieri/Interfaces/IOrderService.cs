using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        List<Order> GetOrderById(Guid OrderId);
        bool AddOrder(Order request);
        bool UpdateOrder(Order request);
        bool DeleteOrder(Guid OrderId);
        void setStatus(Guid orderId, string status);
        void assignCourierToOrder(Guid orderId, Guid courierId);
    }
}
