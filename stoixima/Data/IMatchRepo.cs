using Stoixima.Dtos;
using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    public interface IMatchRepo
    {
        IEnumerable<MatchModel> GetAllMatches();
        MatchModel GetMatchById(int id);
        MatchModel CreateMatch(MatchModel match);
        void DeleteMatch(int id);
        bool UpdateMatch(int id, MatchModel match);
        MatchModel StartMatch(MatchModel match, MatchState state);
    };
}
