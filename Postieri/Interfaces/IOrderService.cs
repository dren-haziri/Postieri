using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        bool AddOrder(Order request);
        bool UpdateOrder(Order request);
        bool DeleteOrder(Guid id);
    }
}
