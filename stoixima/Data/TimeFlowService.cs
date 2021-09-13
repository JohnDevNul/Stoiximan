using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace Stoixima.Data
{
    public class TimeFlowService : ITimeFlowRepo
    {
        private IMatchRepo _matchRepo;
        private readonly Timer _timer;

        public TimeFlowService(IMatchRepo repository)
        {
            _matchRepo = repository;
            _timer = new Timer(TimerCallback, null, 0, 60000);
        }

        private void TimerCallback(Object o)
        {
            var matches = _matchRepo.GetAllMatches();

            foreach (var i in matches)
            {
                var now = DateTime.Now;
                int result = DateTime.Compare(now, i.StartTime);

                if (i.State == MatchState.NotStarted && result == 1)
                {
                    _matchRepo.StartMatch(i, MatchState.FirstHalf);
                }
            }
        }
    }
}
