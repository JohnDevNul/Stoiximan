using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    public class MatchRepo : IMatchRepo
    {
        private List<MatchModel> _matches;

        public MatchRepo()
        {
            _matches = new List<MatchModel>
            {
            };
        }
        public MatchModel CreateMatch(MatchModel match)
        {
            var maxId = _matches.Select(c => c.Id).Max();

            match.Id = match.Id + 1;

            _matches.Add(match);

            return match;
        }

        public void DeleteMatch(int id)
        {
            var match = _matches.FirstOrDefault(c => c.Id == id);

            if(match != null)
            {
                _matches.Remove(match);
            }
        }

        public IEnumerable<MatchModel> GetAllMatches()
        {
            return _matches;
        }

        public MatchModel GetMatchById(int id)
        {
            return _matches.FirstOrDefault(c => c.Id == id);
        }

        public MatchState StartMatch(MatchModel match, MatchState state)
        {
            var time = match.StartTime;

            return state;
        }

        public bool UpdateMatch(int id, MatchModel match)
        {
            var item = _matches.FirstOrDefault(c => c.Id == id);

            if(item != null)
            {
                _matches.Remove(item);
                item.Id = id;
                _matches.Add(match);

                return true;
            }

            return false;
        }
    }
}
