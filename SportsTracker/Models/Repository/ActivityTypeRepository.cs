using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class ActivityTypeRepository
    {
        public SportsTrackerContext _db = null;

        public ActivityTypeRepository()
        {
            _db = new SportsTrackerContext();
        }

        public List<ActivityType> GetActivityTypes()
        {
            return _db.ActivityTypes.ToList();
        } 
    }
}