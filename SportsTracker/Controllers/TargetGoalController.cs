using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTracker.Filters;
using SportsTracker.Models.DbModel;
using SportsTracker.Models.Repository;
using SportsTracker.Models.ViewModel;
using WebMatrix.WebData;

namespace SportsTracker.Controllers
{
    [InitializeSimpleMembership]
    public class TargetGoalController : Controller
    {
        ActivityRepository _activityRepository = new ActivityRepository();
        TargetGoalRepository _goalRepository = new TargetGoalRepository();
        //
        // GET: /TargetGoal/
    
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult SetGoal()
        {
            return View();
        }

           [HttpPost]
        public ActionResult SetGoal(SetGoal setGoal, int endAfterNumberOfDays)
        {
            setGoal.CreatedOn = DateTime.Now.Date;
            setGoal.EndOn = DateTime.Now.AddDays(endAfterNumberOfDays);
            setGoal.IsAchieved = false;
               setGoal.UserProfileId = WebSecurity.CurrentUserId;
            _goalRepository.Add(setGoal);
            return RedirectToAction("CreateActivity", "Location");
        }

        public ActionResult CheckGoalStatus()
        {
            var activityList = new List<ActivityViewModel>();
            
            var goal = _goalRepository.GetLastIncompleteGoalbyUserId(WebSecurity.CurrentUserId);
            var activities = _activityRepository.GetActivityFromStartDateToEndDate(goal.CreatedOn, goal.EndOn);
            double calorie = 0;
            double distance = 0;
            foreach (var a in activities)
            {
                calorie = calorie + a.Calorie;
                distance = distance + a.Distance;
            }
            bool isCalorieAchieved = calorie >= goal.Calorie;
            bool isDistanceAchieved = distance >= goal.Distance;

            if (isDistanceAchieved && isCalorieAchieved)
            {
                goal.IsAchieved = true;
                _goalRepository.Edit(goal);
            }

            var goalViewModel = new GoalViewModel
            {
                AchievedDistance = distance,
                AchivedCalorie = calorie,
                GoalCalorie = goal.Calorie,
                GaolDistance = goal.Distance,
                IsAchieved = goal.IsAchieved
            };

            return View(goalViewModel);
        }
        //public ActionResult CalculateDistance(SetGoal setGoal)
        //{
        //    return RedirectToAction(CheckDistance());
        //}

        //public ActionResult CheckCalorie()
        //{
            
        //}

        //public ActionResult CheckDistance()
        //{

        //}

    }
}
