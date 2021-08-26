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
    public class TimeFlowRepo : ITimeFlowRepo
    {
        private MatchRepo _matchRepo;
        public MatchState TimeState(MatchModel match, MatchState state)
        {
            Timer timer = new Timer(TimerCallback, null, 0, 60000);

            _matchRepo = new MatchRepo();
            var matches = _matchRepo.GetAllMatches();
            
            foreach(var i in matches)
            {
                if(i.State == MatchState.NotStarted && i.StartTime >= DateTime.Now)
                {
                    state = MatchState.FirstHalf;
                    _matchRepo.StartMatch(match, state);

                    return state;
                }
            }
            return state;
        }

        private static void TimerCallback(Object o)
        {
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
        }
    }
}
