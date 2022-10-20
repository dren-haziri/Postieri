using Postieri.Data;
using Postieri.Models;
using System.Linq;

namespace Postieri.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _context;

        public VehicleService(DataContext context)
        {
            _context = context;
        }

      
        public List<Vehicle> GetVehicles()
        {      
            return  _context.Vehicles.ToList();
        }

      

        public List<Vehicle> GetAllAvailableVehiclesWithoutDefect()
        {
            return  _context.Vehicles.Where(x => x.IsAvailable == true && x.HasDefect == false).ToList();
        }

        public List<Vehicle> GetAllVehiclesWithDefect()
        {       
            return  _context.Vehicles.Where(x => x.HasDefect == true).ToList();
        }

      
        public List<Vehicle> GetVehicleById(int id)
        {               
            return _context.Vehicles.Where(v=>v.Id==id).ToList();
        }

     
        public void PutVehicle(int id, Vehicle vehicle)
        {         
            var _vehicle = _context.Vehicles
                .FirstOrDefault(n => n.Id == id);

            if (_vehicle != null)
            {
                _vehicle.Height = vehicle.Height;
                _vehicle.Length = vehicle.Length;
                _vehicle.Width = vehicle.Width;
                _vehicle.LoadSpace = vehicle.Height * vehicle.Width * vehicle.Length;
                _vehicle.LoadWeight = vehicle.LoadWeight;
                _vehicle.CourierId = vehicle.CourierId;
                _vehicle.Description = vehicle.Description;
                _vehicle.HasDefect = vehicle.HasDefect;
                _vehicle.IsAvailable = vehicle.IsAvailable;
                _vehicle.Type = vehicle.Type;
                _vehicle.PlateNumber = vehicle.PlateNumber;

                 _context.SaveChangesAsync();
            }
        }

        public void  PostVehicle(Vehicle vehicle)
        {
            var _vehicle = new Vehicle()
            {
                Id = vehicle.Id,
                Height = vehicle.Height,
                Length = vehicle.Length,
                Width = vehicle.Width,
                LoadSpace = vehicle.Height * vehicle.Width * vehicle.Length,
                LoadWeight = vehicle.LoadWeight,
                CourierId = vehicle.CourierId,
                Description = vehicle.Description,
                HasDefect = vehicle.HasDefect,
                IsAvailable = vehicle.IsAvailable,
                Type = vehicle.Type,
                PlateNumber = vehicle.PlateNumber,

            };
            _context.Vehicles.Add(_vehicle);
             _context.SaveChangesAsync();
        }

      
        public bool DeleteVehicle(int id)
        {  
            var vehicle =  _context.Vehicles.Find(id);
  
            _context.Vehicles.Remove(vehicle);

            return _context.SaveChanges() > 0;  
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool VehicleInUseHasDefect(int id)
        { 
            var vehicle = _context.Vehicles.Find(id);

            return vehicle.HasDefect == true;
        }

        private int GetDefaultAvailableVehicle()
        {
            return _context.Vehicles.Where(x => x.IsAvailable == true && x.HasDefect == false).FirstOrDefault().Id;
        }

   
        public void ChangeVehicleAsync(string email)
        {
            var courier =  _context.Couriers.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));

            if (VehicleInUseHasDefect(courier.VehicleId))
            {
                courier.VehicleId = GetDefaultAvailableVehicle();
                 _context.SaveChangesAsync();
            }
        }
    }
}
