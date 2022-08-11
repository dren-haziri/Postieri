using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.Interfaces; 


namespace Postieri.Services
{
    public class DispatchService : IDispatchService
    {
         // Database database ;
        // public DispatchController(Database db){
        //     db = database;
        // }
       public string Dispatch(Guid managerId, Guid courierId, Guid ProductId)
        {
              Order order = new Order
                    {
                        Price = 33,
                        ProductId = ProductId,
                        CourierId = courierId,
                        ManagerId = managerId,
                        Date = DateTime.Now
                    };
                
                database.Order.Add(order);
                
                 database.SaveChanges();
            //menager dispatch order to courier 
            return "Porosia u shtua";
        } 

     
    }
}