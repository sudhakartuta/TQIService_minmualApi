using DataAccess.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _Context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _Context = context;
            _logger = logger;
        }

       

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            _logger.LogInformation("Calling GetAllUsers() method");
            try
            {            
                IEnumerable<User> users = await _Context.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllUsers() method",ex.Message);
                return null;
            }
        }

        public async Task<User> GetUser(int ID)
        {
            _logger.LogInformation("Calling GetUser() method");
            User user=new User();
            try
            {

                user = await _Context.Users.FirstOrDefaultAsync(r => r.Id == ID);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllUsers() method", ex.Message);
                return user;
            }
        }

        public async Task<User> SaveUser(User user)
        {
            _logger.LogInformation("Calling SaveUser() method");

            try
            {
                await _Context.Users.AddAsync(user);
                _Context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllUsers() method", ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            _logger.LogInformation("Calling UpdateUser() method");

            try
            {
                User finduser= await _Context.Users.FirstOrDefaultAsync(r=>r.Id == user.Id);
                if (finduser != null)
                {
                    finduser.Username = user.Username;
                    finduser.Password = user.Password;
                    finduser.Name = user.Name;
                    finduser.Access_level = user.Access_level;
                    finduser.Payment_mode = user.Payment_mode;
                    finduser.Lockeddate = user.Lockeddate;
                    finduser.Status = user.Status;
                    _Context.SaveChanges();
                    return true;
                    _logger.LogInformation($"{user.Name} Data Updated Successfully");
                }
                else
                {
                    _logger.LogInformation($"{user.Name} user not found..");
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in UpdateUser() method", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(int ID)
        {
            _logger.LogInformation("Calling DeleteUser() method");
            try
            {

                User finduser = await _Context.Users.FirstOrDefaultAsync(r => r.Id == ID);
                if (finduser != null)
                {
                    _Context.Users.Remove(finduser);
                    _Context.SaveChanges();

                    _logger.LogInformation($"{finduser.Name} user removed successfully..");
                    return true;
                }
                else { 
                    _logger.LogInformation($"{finduser.Name} user not found..");
                    return false;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error in DeleteUser() method", ex.Message);
                return false;
            }
        }
    }
}
