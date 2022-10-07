using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IDeliveryPriceService
    {
        Task<List<DeliveryPrice>> Get();
        Task<List<DeliveryPrice>> AddCalculatePrice(DeliveryPrice request);
        Task<List<DeliveryPrice>> UpdateCalculatePrice(DeliveryPrice request);
        Task<List<DeliveryPrice>> Delete(Guid DeliveryPriceId);
    }
}
