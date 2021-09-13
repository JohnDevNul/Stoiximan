using Stoixima.Enums;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; }
        public double Time { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public GoalModel[] Goals { get; set; }
        public MatchState State { get; set; }
        public CornerModel[] Corners { get; set; }
        public CardModel[] Cards { get; set; }
        public DateTime StartTime { get; set; }
    }
}

