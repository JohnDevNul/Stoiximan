using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    public interface IUserRepo
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        UserModel CreateUser(UserModel item);
        void DeleteUser(int id);    
        bool UpdateUser(int id, UserModel item);
    }
}
