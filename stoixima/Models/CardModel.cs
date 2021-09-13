using Stoixima.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public double Time { get; set; }
        public TeamSide Side { get; set; }
    }
}
