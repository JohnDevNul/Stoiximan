using Stoixima.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        public string MatchName => $"{Home.Name} vs {Away.Name}";
        public double Time { get; set; }
        public TeamModel Home { get; set; }
        public TeamModel Away { get; set; }
        public GoalModel[] Goals { get; set; }
        public MatchState State { get; set; }
        public CornerModel[] Corners { get; set; }
        public Cards[] Cards { get; set; }
        public DateTime StartTime { get; set; }
    }
}
