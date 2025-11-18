using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class UserRepository
    {
        ClothesDbContext _context;
        public UserRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<User> GetByKeyAndPassword(string username, string hashPassword)
        {
            User availableUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (availableUser == null)
            {
                return null;
            }
            else
            {
                if (availableUser.PasswordHash.ToUpper() == hashPassword.ToUpper())
                {
                    return availableUser;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                var addedUser = _context.Users.Add(user).Entity;
                await _context.SaveChangesAsync();
                return addedUser;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(user.UserID);

                if (existingUser == null) return null;

                existingUser.UserName = user.UserName;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.Role = user.Role;
                existingUser.Status = user.Status;

                var updatedUser = _context.Users.Update(existingUser).Entity;
                await _context.SaveChangesAsync();
                return updatedUser;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null) return null;


                var updatedUser = _context.Users.Update(user).Entity;
                await _context.SaveChangesAsync();
                return updatedUser;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> FindUserExistByKeyWord(string key)
        {
            try
            {
                var u = _context.Users.FirstOrDefault(x => x.UserName == key);

                if (u == null)
                    return null;
                return u;
            }
            catch
            {
                return null;
            }
        }
    }
}

