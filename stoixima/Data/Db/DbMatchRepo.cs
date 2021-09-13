using Dapper;
using Microsoft.Extensions.Configuration;
using Stoixima.Dtos;
using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data.Db
{
    public class DbMatchRepo : IMatchRepo
    {
        private readonly IConfiguration _configuration;
        private readonly ITimeFlowRepo _timeFlowRepo;

        public DbMatchRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MatchModel CreateMatch(MatchModel match)
        {
            var matchDto = new MatchDto();
            
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.QuerySingle<int>("INSERT INTO Match VALUES (@time, @home, @away, @state, @goals, @corners, @cards, @starttime);SELECT CAST(SCOPE_IDENTITY() as int)", new { Time = match.Time, Home = match.Home, Away = match.Away, State = match.State, Goals = match.Goals, Corners = match.Corners, Cards = match.Cards, StartTime = match.StartTime });

            connection.Close();

            match.Id = result;

            return match;
        }

        public void DeleteMatch(int id)
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

        public IEnumerable<MatchModel> GetAllMatches()
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query<MatchModel>("SELECT * FROM Teams");

            connection.Close();

            return result;
        }

        public MatchModel GetMatchById(int id)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query<MatchModel>("SELECT * FROM Teams WHERE Id = @Id", new { Id = id });

            connection.Close();

            return result.FirstOrDefault();
        }

        public MatchModel StartMatch(MatchModel match, MatchState state)
        {
            var start = _timeFlowRepo;

            return match;
        }

        public bool UpdateMatch(int id, MatchModel match)
            {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.Query("UPDATE Teams SET MatchName = @matchname, Time = @time, Home = @home, Away = @away, State = @state, Goals = @goals, Corners = @corners, Cards = @cards, StartTime = @starttime WHERE Id = @id", new { MatchName = match.MatchName, Time = match.Time, Home = match.Home, Away = match.Away, State = match.State, Goals = match.Goals, Corners = match.Corners, Cards = match.Cards, StartTime = match.StartTime, Id = match.Id });

            connection.Close();

            return true;
        }
    }
}
