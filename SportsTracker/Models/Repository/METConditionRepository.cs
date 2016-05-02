using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class METConditionRepository
    {
        SportsTrackerContext db = new SportsTrackerContext();

        public double GetMET(double speed, int activityTypeId)
        {
            //var speedInMph = speed;
            var speedInMph = speed * .62137;
            var met = (from mc in db.MetConditions
                where
                    (mc.ActivityTypeId == activityTypeId && (speedInMph >= mc.StartLimit && speedInMph <= mc.EndLimit))
                select mc.MET).FirstOrDefault();
            return met;
        }
    }
}