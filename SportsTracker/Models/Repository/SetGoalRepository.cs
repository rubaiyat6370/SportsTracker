using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class SetGoalRepository
    {
        public SportsTrackerContext db = null;

        public SetGoalRepository()
        {
            db = new SportsTrackerContext();
        }

    }
}