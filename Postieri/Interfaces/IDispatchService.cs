using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Interfaces
{
    public interface IDispatchService
    {
         string Dispatch(Guid managerId, Guid courierId, Guid ProductId);
    }
}