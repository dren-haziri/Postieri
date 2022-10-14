using System;
using Postieri.DTOs;
using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface ICourierService
    {
        void UpdateStatus(Guid orderId, string status);
        List<Order> AcceptOrder(Guid order, Guid courierId);
        void DeclineOrder(Guid orderId);
    }
}
