using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IDeliveryPriceService
    {
        List<DeliveryPrice> GetCalculations();
        bool AddCalculation(DeliveryPrice request);
        bool DeleteCalculation(Guid DeliveryPriceId);
    }
}
