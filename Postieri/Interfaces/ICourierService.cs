using System;
using Postieri.DTOs;

namespace Postieri.Interfaces
{
    public interface ICourierService
    {
        void UpdateStatus(Guid orderId, string status);
    }
}
