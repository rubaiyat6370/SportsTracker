using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class TargetGoalRepository
    {
        readonly SportsTrackerContext _db = new SportsTrackerContext();
        public SetGoal GetLastIncompleteGoalbyUserId(int userId)
        {
            /*var goal =
                _db.SetGoals.Where(g => g.UserProfileId == userId && g.IsComplete == false)
                    .Select(g => g)
                    .FirstOrDefault();*/

            var goal = _db.SetGoals.Where(g => g.UserProfileId == userId).OrderByDescending(g => g.Id)
                .Select(g=> g);

            return goal.FirstOrDefault();
        }

        public bool Edit(SetGoal goal)
        {
            _db.SetGoals.Attach(goal);
            _db.Entry(goal).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        public bool Add(SetGoal goal)
        {
            _db.SetGoals.Add(goal);
            return _db.SaveChanges() > 0;
        }
    }
}