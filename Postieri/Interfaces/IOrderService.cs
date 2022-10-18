using Microsoft.AspNetCore.Mvc;
using Postieri.DTO;
using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IOrderService
    {
        Order GetOrder(Guid id);
        ActionResult<List<Order>> GetAllOrders();
        bool PostOrder(OrderDto order);
        bool DeleteOrder(Guid OrderId);
        void setStatus(Guid orderId, string status, Guid courier);
        void assignCourierToOrder(Guid orderId, Guid courierId);
    }
}
