using System.Collections.Generic;
using System.Threading.Tasks;
using epos.Models;

namespace epos.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        ICollection<User> GetUsers();
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(long UserID);
        Task<User> GetUserById(long UserID);
        Task<User> GetUserByName(string name);

    }
}