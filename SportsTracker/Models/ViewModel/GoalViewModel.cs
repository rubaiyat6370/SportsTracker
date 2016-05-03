using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.ViewModel
{
    public class GoalViewModel
    {
        public double GoalCalorie { get; set; }
        public double GaolDistance { get; set; }

        public double AchivedCalorie { get; set; }
        public double AchievedDistance { get; set; }

        public bool IsAchieved { get; set; }

    }
}