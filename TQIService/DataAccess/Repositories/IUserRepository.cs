using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUser(int ID);
        public Task<User> SaveUser(User user);
        public Task<bool> DeleteUser(int ID);
        public Task<bool> UpdateUser(User user);
        public Task<User> Login(string userName, string password);
    }
}
