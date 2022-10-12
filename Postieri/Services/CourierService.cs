using Postieri.Data;
using Postieri.DTOs;


namespace Postieri.Services
{
    public class CourierService : ICourierService
    {
        private readonly DataContext _dbcontext;

        public CourierService(DataContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
     
        public void UpdateStatus(Guid orderId, string status)
        {
            var order = _dbcontext.Orders.Find(orderId);
            if(order != null)
            {
                 order.Status = status;
                _dbcontext.SaveChanges();
            }
        }
    }
}
