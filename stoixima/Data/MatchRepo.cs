using Stoixima.Dtos;
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
            if (_matches != null && _matches.Count > 0)
            {
                var maxId = _matches.Select(c => c.Id).Max();

                match.Id = maxId + 1;

                _matches.Add(match);

                return match;
            }
            match.Id = 1;

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

        public MatchModel StartMatch(MatchModel match, MatchState state)
        {
            match.State = MatchState.FirstHalf;

            return match;
        }
    }
}
