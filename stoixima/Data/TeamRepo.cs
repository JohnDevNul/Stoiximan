using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    public class TeamRepo : ITeamRepo
    {
        private List<TeamModel> _teams;
        public TeamRepo()
        {
            _teams = new List<TeamModel>
            {
                new TeamModel { Id = 0, Name = "Olimpiakos", Points = 10 },
                new TeamModel { Id = 1, Name = "Panatha", Points = 7 },
                new TeamModel { Id = 2, Name = "Aek", Points = 3 }
            };
        }
        public TeamModel CreateTeam(TeamModel team)
        {
            var maxId = _teams.Select(c => c.Id).Max();

            team.Id = maxId + 1;

            _teams.Add(team);

            return team;
        }

        public void DeleteTeam(int id)
        {
            var team = _teams.FirstOrDefault(c => c.Id == id);

            if (team != null)
            {
                _teams.Remove(team);
            }
        }

        public IEnumerable<TeamModel> GetAllTeams()
        {
            return _teams;        
        }

        public TeamModel GetTeamById(int id)
        {
            return _teams.Find(c => c.Id == id);
        }

        public bool UpdateTeam(int id, TeamModel teamModel)
        {
            var team = _teams.Find(c => c.Id == id);

            if (team != null)
            {
                _teams.Remove(team);
                teamModel.Id = id;
                _teams.Add(teamModel);

                return true;
            }

            return false;
        }
    }
}
