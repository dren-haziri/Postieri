using Postieri.Models;

namespace Postieri.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehicles();
        List<Vehicle> GetVehicleById(int id);
        List<Vehicle> GetAllAvailableVehiclesWithoutDefect();
        public List<Vehicle> GetAllVehiclesWithDefect();
        void PutVehicle(int id, Vehicle vehicle);
        void PostVehicle(Vehicle vehicle);
        bool DeleteVehicle(int id);
        void ChangeVehicleAsync(string email);


    }
}