using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    public interface ITeamRepo
    {
        IEnumerable<TeamModel> GetAllTeams();
        TeamModel GetTeamById(int id);
        TeamModel CreateTeam(TeamModel item);
        void DeleteTeam(int id);
        bool UpdateTeam(int id, TeamModel item);
    }
}
