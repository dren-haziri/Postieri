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
            return _context.DeliveryPrices.ToList();
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
                if (deliveryPrice.CountryTo.ToUpper() == "Kosove".ToUpper())
                {
                    deliveryPrice.TotalPrice = 2;
                }
                else if (deliveryPrice.CountryTo.ToUpper() == "Shqiperi".ToUpper())
                {
                    deliveryPrice.TotalPrice = 3;
                }
                else if (deliveryPrice.CountryTo.ToUpper() == "Maqedoni".ToUpper())
                {
                    deliveryPrice.TotalPrice = 4;
                }
                else if (deliveryPrice.CountryTo.ToUpper() == "Montenegro".ToUpper())
                {
                    deliveryPrice.TotalPrice = 4;
                }
                else
                {
                    deliveryPrice.TotalPrice = 20;
                }

                if (deliveryPrice.Dimension.name.ToLower() == "largepackage" || deliveryPrice.Dimension.name.ToLower() == "large")
                {
                    deliveryPrice.TotalPrice *= 3;
                }
                else if (deliveryPrice.Dimension.name.ToLower() == "mediumpackage" || deliveryPrice.Dimension.name.ToLower() == "medium")
                {
                    deliveryPrice.TotalPrice *= 2;
                }
                else if (deliveryPrice.Dimension.name.ToLower() == "smallpackage" || deliveryPrice.Dimension.name.ToLower() == "small")
                {
                    deliveryPrice.TotalPrice *= 1;
                }
                else
                {
                    deliveryPrice.TotalPrice *= 5;
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
