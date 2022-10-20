using System;
using Postieri.DTOs;
using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface ICourierService
    {
        void UpdateStatus(Guid orderId, string status);
        bool AcceptOrder(Guid order, Guid courierId);
        bool DeclineOrder(Guid orderId, Guid courierId);
    }
}
