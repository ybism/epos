using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using epos.DAL;
using epos.Models;
using epos.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace epos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodItemContext _db;

        public UserRepository(FoodItemContext db)
        {
            _db = db;
        }

        public async Task<User> CreateUser(User user)
        {
            await _db.Users.AddAsync(user);
            await Save();
            
            return user;
        }

        public async Task<User> GetUserById(long UserID)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == UserID);

            if (user == null)
            {
                throw new NullReferenceException("user does not exist");
            }

            return user;

        }

        public async Task<User> GetUserByName(string name)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Name == name);

            if (user == null)
            {
                throw new NullReferenceException("User does not exist");
            }

            return user;
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        public async Task<User> UpdateUser(User user)
        {
            var userUpdate = await _db.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);

            if (user == null)
            {
                throw new NullReferenceException("User does not exist");
            }

            userUpdate.Name = user.Name;
            userUpdate.Address = user.Address;

            _db.Users.Update(userUpdate);

            await Save();

            return user;
        }

        public async Task<User> DeleteUser(long UserID)
        {
            var userToDelete = await _db.Users.FirstOrDefaultAsync(x => x.UserId == UserID);

            if (userToDelete == null)
            {
                throw new NullReferenceException("User does not exist");
            }

            _db.Users.Remove(userToDelete);
            await Save();

            return userToDelete;
        }

        private async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}