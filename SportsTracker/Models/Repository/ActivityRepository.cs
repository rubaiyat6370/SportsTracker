using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;
using WebMatrix.WebData;
using Activity = System.EnterpriseServices.Activity;

namespace SportsTracker.Models.Repository
{
    public class ActivityRepository
    {
        public SportsTrackerContext _db = null;

        public ActivityRepository()
        {
            _db = new SportsTrackerContext();
        }

        public bool Add(DbModel.Activity activity)
        {
            _db.Activities.Add(activity);
            return _db.SaveChanges() > 0;
        }

        public List<DbModel.Activity> GetMyActivities()
        {
            var activities = _db.Activities.Where(acticity => (acticity.UserProfileId == WebSecurity.CurrentUserId)).ToList();

            return activities;

        }

        public DbModel.Activity Get(int id)
        {
            var activity = _db.Activities.Find(id);
            return activity;

        }
    }
}