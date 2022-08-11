using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postieri.Models
{
    public class User
    {
        public Guid Id {get;set;}
        public Guid Username {get;set;}
        public Role RoleId {get;set;}
    }
}