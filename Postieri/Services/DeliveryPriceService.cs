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
        public async Task<List<DeliveryPrice>> Get()
        {
            return await _context.DeliveryPrices.ToListAsync();
        }
        public async Task<List<DeliveryPrice>> AddCalculatePrice(DeliveryPrice request)
        {
            _context.DeliveryPrices.Add(request);
            await _context.SaveChangesAsync();
            return await _context.DeliveryPrices.ToListAsync();
        }
        public async Task<List<DeliveryPrice>> UpdateCalculatePrice(DeliveryPrice request)
        {
            var deliceryPrice = await _context.DeliveryPrices.FindAsync(request.DeliveryPriceId);
            deliceryPrice.CountryTo = request.CountryTo;
            deliceryPrice.CityTo = request.CityTo;
            deliceryPrice.PostCodeTo = request.PostCodeTo;
            deliceryPrice.Dimension = request.Dimension;
            deliceryPrice.TotalPrice = request.TotalPrice;

            await _context.SaveChangesAsync();
            return await _context.DeliveryPrices.ToListAsync();
        }
        public async Task<List<DeliveryPrice>> Delete(Guid DeliveryPriceId)
        {
            var deliceryPrice = await _context.DeliveryPrices.FindAsync(DeliveryPriceId);

            _context.DeliveryPrices.Remove(deliceryPrice);
            await _context.SaveChangesAsync();
            return await _context.DeliveryPrices.ToListAsync();
        }
    }
}
