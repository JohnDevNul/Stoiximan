using Dapper;
using Microsoft.Extensions.Configuration;
using RestSharp;
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
        private readonly ITeamRepo _team;
        private readonly object _timeFlowRepo;

        public DbMatchRepo(IConfiguration configuration, ITeamRepo team)
        {
            _configuration = configuration;
            _team = team;
        }

        public MatchModel CreateMatch(MatchModel match)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.QuerySingle<int>("INSERT INTO Matches VALUES (@time, @homeTeamId, @awayTeamId, @state, @starttime);" +
                                                     "SELECT CAST(SCOPE_IDENTITY() as int)", 
                                                     new { Time = match.Time, HomeTeamId = match.Home.Id, AwayTeamId = match.Away.Id, State = match.State, StartTime = match.StartTime });

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

            var result = connection.Query("DELETE FROM Matches WHERE Id = @id", new { Id = id });

            connection.Close();
        }
        public MatchModel GetMatchById(int id)
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var result = connection.QuerySingle<MatchDto>("SELECT * FROM Matches WHERE Id = @Id", new { Id = id });
            var home = connection.QuerySingle<TeamModel>("SELECT HomeTeamId FROM Matches WHERE Id = @Id", new { Id = id });
            var away = connection.QuerySingle<TeamModel>("SELECT AwayTeamId FROM Matches WHERE Id = @Id", new { Id = id });

            connection.Close();

            MatchModel match = new MatchModel()
            {
                Time = result.Time,
                Home = home,
                Away = away,
                State = result.State,
                Goals = result.Goals,
                Corners = result.Corners,
                Cards = result.Cards,
                StartTime = result.StartTime
            };

            return match;
        }

        public async Task<IEnumerable<MatchModel>> GetAllMatches()
        {
            var conStr = _configuration["DbConnection"];

            var connection = new SqlConnection(conStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            
            var result = await connection.QueryAsync<MatchModel>("SELECT * FROM Matches"); 

            foreach (var i in result)
            {
                var home = connection.QuerySingle<TeamModel>("SELECT HomeTeamId FROM Matches");
                var away = connection.QuerySingle<TeamModel>("SELECT AwayTeamId FROM Matches");
                
                var match = new MatchModel()
                {
                    Time = i.Time,
                    Home = home,
                    Away = away,
                    State = i.State,
                    Goals = i.Goals,
                    Corners = i.Corners,
                    Cards = i.Cards,
                    StartTime = i.StartTime
                };
            }

            connection.Close(); 

            return result.ToList();
        }

        public IEnumerator<Task> GetEnumerator()
        {
            throw new NotImplementedException();
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

            var result = connection.Query("UPDATE Matches (Time, HomeTeamId, AwayTeamId, State, StartTime) SET Time = @time, HomeTeamId = @home, AwayTeamId = @away, State = @state, StartTime = @starttime WHERE Id = @id", new { Time = match.Time, HomeTeamId = match.Home.Id, AwayTeamId = match.Away.Id, State = match.State, StartTime = match.StartTime});

            connection.Close();

            return true;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
