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
    public class DbTeamRepo : ITeamRepo
    {
        private readonly IConfiguration _configuration;

        public DbTeamRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TeamModel CreateTeam(TeamModel item)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.QuerySingle<int>("INSERT INTO Teams (name, points) VALUES (@name, @points);SELECT CAST(SCOPE_IDENTITY() as int)", new {Id = item.Id, Name = item.Name, Points = item.Points});

            connection.Close();

            item.Id = result;

            return item;
        }

        public void DeleteTeam(int id)
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

        public IEnumerable<TeamModel> GetAllTeams()
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query<TeamModel>("SELECT * FROM Teams");

            connection.Close();

            return result;
        }

        public TeamModel GetTeamById(int id)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query<TeamModel>("SELECT * FROM Teams WHERE Id = @Id", new { Id = id });

            connection.Close();

            return result.FirstOrDefault();
        }

        public bool UpdateTeam(int id, TeamModel item)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query("UPDATE Teams SET Name = @name, Points = @points WHERE Id = @id", new { Name = item.Name, Points = item.Points, Id = item.Id});

            connection.Close();

            return true;
        }
    }
}
