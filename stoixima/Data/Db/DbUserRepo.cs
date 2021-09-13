using Dapper;
using Microsoft.Extensions.Configuration;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data.Db
{
    public class DbUserRepo : IUserRepo
    {
        private readonly IConfiguration _configuration;

        public DbUserRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserModel CreateUser(UserModel item)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.QuerySingle<int>("INSERT INTO Users VALUES (@firstname, @lastname, @password, @sex, @date, @address, @email, @phone);SELECT CAST(SCOPE_IDENTITY() as int)", new { FirstName = item.FirstName, LastName = item.LastName, Password = item.Password, Sex = item.Sex, Date = item.Date, Address = item.Address, Email = item.Email, Phone = item.Phone });

            connection.Close();

            item.Id = result;

            return item;
        }

        public void DeleteUser(int id)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query("DELETE FROM Teams WHERE Id = @id", new { Id = id });

            connection.Close();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query<UserModel>("SELECT * FROM Teams");

            connection.Close();

            return result;
        }

        public UserModel GetUserById(int id)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query<UserModel>("SELECT * FROM Teams WHERE Id = @Id", new { Id = id });

            connection.Close();

            return result.FirstOrDefault();
        }

        public bool UpdateUser(int id, UserModel item)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query("UPDATE Teams SET Firstname = @firstname, LastName = @lastname, Password = @password, Sex = @sex, Date = @date, Address = @address, Email = @email, Phone = @phone WHERE Id = @id", new { FirstName = item.FirstName, LastName = item.LastName, Password = item.Password, Sex = item.Sex, Date = item.Date, Address = item.Address, Email = item.Email, Phone = item.Phone, Id = item.Id });

            connection.Close();

            return true;
        }
    }
}
