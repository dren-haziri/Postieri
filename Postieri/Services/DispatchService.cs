using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.Interfaces; 
using System.Runtime.InteropServices;


namespace Postieri.Services
{
    public class DispatchService : IDispatchService
    {
       
       public string TransferPackage(Guid managerId, Guid courierId, Guid productId)
        {
              Order order = new Order
                    {
                        Price = 33,
                        ProductId = productId,
                        CourierId = courierId,
                        ManagerId = managerId,
                        Date = DateTime.Now
                    };

                //database.Order.Add(order);

                 //database.SaveChanges();
            //menager dispatch order to courier 
            return "Porosia u shtua.";
        }

        public string VerifyAddress(Guid addressTo){

           Order order = new Order{ AddressTo = addressTo};

            if(addressTo == null){
                return "Adresa nuk mund te jete e zbrazet";
            }
            
            return "Verified Address";
        }

    }
}