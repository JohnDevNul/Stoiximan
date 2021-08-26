using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    public class UserRepo : IUserRepo
    {
        private List<UserModel> _users;
        public UserRepo()
        {
            _users = new List<UserModel>
            {
                new UserModel {Id=0, FirstName="John", LastName="Zolo", Password="12345", Sex="Male", Date="13/5/97", Address="Ano Kerasovo", Email="yahoo", Phone="6947"},
                new UserModel {Id=1, FirstName="Thana", LastName="Zolo", Password="123456", Sex="Male", Date="20/7/98", Address="Ano Kerasovo", Email="google", Phone="6947"}
            };
        }

        public UserModel CreateUser(UserModel user)
        {
            var maxId = _users.Select(c => c.Id).Max();

            user.Id = maxId + 1;

            _users.Add(user);

            return user;
        }

        public void DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(c => c.Id == id);
            if(user != null)
            {
                _users.Remove(user);
            }
        }

        public IEnumerable<UserModel> GetAllUsers()
        { 

            return _users;
        }

        public UserModel GetUserById(int id)
        {
            return _users.Find(c => c.Id == id);
        }

        public bool Predicate(UserModel user, int id)
        {
            return user.Id == id;
        }

        public bool UpdateUser(int id, UserModel userModel)
        {
            var user = _users.FirstOrDefault(c=> c.Id == id);

            if(user != null)
            {
                _users.Remove(user);
                userModel.Id = id;
                _users.Add(userModel);

                return true;
            }

            return false;
        }

    }

    public static class Extensions
    {
        public static UserModel OurFind(this List<UserModel> list, Func<UserModel, bool> predicate)
        {
            foreach (var user in list)
            {
                if (predicate.Invoke(user))
                {
                    return user;
                }
            }

            return null;
        }
    }
}
