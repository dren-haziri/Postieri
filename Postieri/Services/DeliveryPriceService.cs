using Postieri.Data;
using Postieri.Models;
using Microsoft.EntityFrameworkCore;

namespace Postieri.Services
{
    public class DeliveryPriceService : IDeliveryPriceService
    {
        private readonly DataContext _context;
        public DeliveryPriceService(DataContext context)
        {
            _context = context;
        }
        public List<DeliveryPrice> GetCalculations()
        {
            return _context.DeliveryPrices.Include(x=> x.Dimension).ToList();
        }
        public bool AddCalculation(DeliveryPrice request)
        {
            var deliveryPrice = new DeliveryPrice()
            {
                DeliveryPriceId = request.DeliveryPriceId,
                CountryTo = request.CountryTo,
                CityTo = request.CityTo,
                PostCodeTo = request.PostCodeTo,
                Dimension = request.Dimension,
                TotalPrice = request.TotalPrice
            };
            if (deliveryPrice == null)
            {
                return false;
            }
            else
            {
                deliveryPrice.TotalPrice = 2;

                if (deliveryPrice.Dimension.width > 120 || deliveryPrice.Dimension.height > 80 || deliveryPrice.Dimension.length > 80)
                {
                    return false;
                }
                else if(deliveryPrice.Dimension.width > 80 || deliveryPrice.Dimension.height > 60 || deliveryPrice.Dimension.length > 60)
                {
                    deliveryPrice.Dimension.name = "Large Package";
                    deliveryPrice.TotalPrice *= 3;
                }
                else if (deliveryPrice.Dimension.width > 40 || deliveryPrice.Dimension.height > 30 || deliveryPrice.Dimension.length > 30)
                {
                    deliveryPrice.Dimension.name = "Medium Package";
                    deliveryPrice.TotalPrice *= 2;
                }
                else
                {
                    deliveryPrice.Dimension.name = "Small Package";
                    deliveryPrice.TotalPrice *= 1;
                }

                _context.DeliveryPrices.Add(deliveryPrice);
                _context.SaveChanges();
                return true;
            }
        }
        public bool DeleteCalculation(Guid DeliveryPriceId)
        {
            var deliveryPrice = _context.DeliveryPrices.Find(DeliveryPriceId);
            if (deliveryPrice == null)
            {
                return false;
            }
            else if (!CalculationExists(deliveryPrice))
            {
                return false;
            }
            else
            {
                _context.DeliveryPrices.Remove(deliveryPrice);
                _context.SaveChanges();
                return true;
            }
        }
        public bool CalculationExists(DeliveryPrice request)
        {
            bool alreadyExist = _context.DeliveryPrices.Any(x => x.DeliveryPriceId == request.DeliveryPriceId);
            return alreadyExist;
        }
    }
}
