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

            };
        }
        public TeamModel CreateTeam(TeamModel team)
        {
            if (_teams != null && _teams.Count > 0)
            {
                var maxId = _teams.Select(c => c.Id).Max();

                team.Id = maxId + 1;

                _teams.Add(team);

                return team;
            }

            team.Id = 1;

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
