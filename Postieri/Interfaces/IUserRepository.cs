using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Postieri.DTOs;
using Postieri.Models;
using Postieri.Helpers;

namespace Postieri.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<PagedList<UserDto>> GetUsersAsync(User user);
        Task<UserDto> GetUserAsync(string username);
    }
}
