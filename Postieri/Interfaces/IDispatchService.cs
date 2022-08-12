using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Interfaces
{
    public interface IDispatchService
    {
         string TransferPackage(Guid managerId, Guid courierId, Guid productId);
         string VerifyAddress(Guid addressTo);
    }
}