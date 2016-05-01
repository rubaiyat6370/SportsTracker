using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;
using SportsTracker.Models.ViewModel;
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

        public bool DeleteActivity(int id)
        {
            var activity = _db.Activities.Find(id);
            _db.Activities.Attach(activity);
            _db.Entry(activity).State = EntityState.Deleted;
            return _db.SaveChanges() > 0;
        }

        public List<ActivityViewModel> GetDataFromDateRange(int numberOfDays)
        {
            var d = DateTime.Today.AddDays(-numberOfDays);
            var activities = (from ac in _db.Activities
                              join activityType in _db.ActivityTypes on ac.ActivityTypeId equals activityType.Id
                              where (ac.CreatedOn > d && ac.UserProfileId==WebSecurity.CurrentUserId)
                              select new ActivityViewModel
                              {
                                  Id = ac.Id,
                                  ActivityTypeTitle = ac.ActivityType.Title,
                                  ActivityTypeId = ac.ActivityTypeId,
                                  CreatedOn = ac.CreatedOn,
                                  Duration = ac.Duration,
                                  Speed = ac.Speed,
                                  Distance = ac.Distance,
                                  Calorie = ac.Calorie,
                                  FilePath = ac.FilePath,
                                  UserProfileId = ac.UserProfileId
                              });
            var activityList = from activityViewModel in activities
                group activityViewModel by EntityFunctions.TruncateTime(activityViewModel.CreatedOn);
            
            var acList = new List<ActivityViewModel>();
            foreach (var group in activityList)
            {
                double distance = 0;
                double cal = 0;
                double duration = 0;
                
                foreach (var item in group)
                {
                    distance = distance + item.Distance;
                    cal = cal + item.Calorie;
                    duration = duration + item.Duration;
                }

                acList.Add(new ActivityViewModel
                {
                    CreatedOn = group.Key,
                    Calorie = cal,
                    Distance = distance,
                    Duration = duration
                });
            }
            return acList.ToList();
        }
    }
}