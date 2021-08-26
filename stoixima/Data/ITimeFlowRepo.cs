using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Data
{
    interface ITimeFlowRepo
    {
        MatchState TimeState(MatchModel match, MatchState state);
    }
}
