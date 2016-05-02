using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using SportsTracker.Filters;
using SportsTracker.Models;
using SportsTracker.Models.DbModel;
using SportsTracker.Models.Repository;
using SportsTracker.Models.ViewModel;
using WebMatrix.WebData;

namespace SportsTracker.Controllers
{
    [InitializeSimpleMembership]
    public class LocationController : Controller
    {
        private UserRepository userRepository = new UserRepository();
        private ActivityRepository _activityRepository = new ActivityRepository();
        private ActivityTypeRepository _activityTypeRepository = new ActivityTypeRepository();
        private METConditionRepository _metConditionRepository = new METConditionRepository();
        //
        // GET: /Location/

        public ActionResult Index()
        {
            var gpsDataList = new List<GpsData>();
            var xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Server.MapPath("~/Content/gps File/120.gpx"));
            XmlNodeList trackPointNodes = xmldoc.GetElementsByTagName("trkpt");
            foreach (XmlNode node in trackPointNodes)
            {
                if (node.Attributes != null)
                {
                    string lat = node.Attributes["lat"].Value;
                    var lng = node.Attributes["lon"].Value;
                    var ele = node.ChildNodes[0].InnerText;
                    var time = node.ChildNodes[1].InnerText;
                    var a = DateTime.Now;

                    gpsDataList.Add(new GpsData
                    {
                        Latitude = lat,
                        Longitude = lng,
                        Elevation = ele,
                        Time = DateTime.Parse(time, null, DateTimeStyles.RoundtripKind)
                        //Call the DateTime.Parse(String, IFormatProvider, DateTimeStyles) method,
                        //and pass DateTimeStyles.RoundtripKind as the value of the styles parameter.
                    });
                }
            }

            double distance = 0;
            for (int i = 0; i < gpsDataList.Count - 1; i++)
            {
                if (i + 1 == gpsDataList.Count - 1) break;
                var sLat = double.Parse(gpsDataList.ElementAt(i).Latitude);
                var sLng = double.Parse(gpsDataList.ElementAt(i).Longitude);
                var eLat = double.Parse(gpsDataList.ElementAt(i + 1).Latitude);
                var eLng = double.Parse(gpsDataList.ElementAt(i + 1).Longitude);
                distance = distance + Calc(sLat, sLng, eLat, eLng);
            }
            var finalDistance = Math.Round(distance, 2);

            var timeStamp = Math.Round((gpsDataList.Last().Time - gpsDataList.First().Time).TotalHours, 2);
            var avgSpeed = Math.Round(distance/timeStamp, 2);

            var locationViewModel = new LocationViewModel();
            locationViewModel.GpsDatas = gpsDataList;
            locationViewModel.Distance = finalDistance;
            locationViewModel.Speed = avgSpeed;
            locationViewModel.Time = timeStamp;

            //get other user information from user table to caclculate calorie burn

            //var currentUser = Membership.GetUser(User.Identity.Name);
            ////string username = currentUser.UserName;//** get UserName
            //Guid userID = (Guid) currentUser.ProviderUserKey; //** get user ID
            //string aa = userID.ToString();
            //int intuserID = int.Parse(aa);

            //// int bb=int.pars(aa);
            var user = userRepository.GetUserByProfileId(WebSecurity.CurrentUserId);
            //var user = userRepository.GetUserByProfileId(intuserID);
            var weight = user.Weight;
            var cal = Convert.ToDouble(weight)*.8*avgSpeed;

            locationViewModel.BurnedCalorie = cal;

            //@ViewBag.Fabiha = locationViewModel;
            return View(locationViewModel);
        }


        public static double Calc(double lat1,
            double long1, double lat2, double long2)
        {

            double dDistance = Double.MinValue;
            double dLat1InRad = lat1*(Math.PI/180.0);
            double dLong1InRad = long1*(Math.PI/180.0);
            double dLat2InRad = lat2*(Math.PI/180.0);
            double dLong2InRad = long2*(Math.PI/180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude/2.0), 2.0) +
                       Math.Cos(dLat1InRad)*Math.Cos(dLat2InRad)*
                       Math.Pow(Math.Sin(dLongitude/2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0*Math.Asin(Math.Sqrt(a));

            //c = 2 * atan2(sqrt(a), sqrt(1-a)) 
            //double c = 2.0 * Math.Atan2(Math.Pow(a, 2), Math.Pow((1 - a), 2));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms*c;

            return dDistance;
        }



        public ActionResult Direction()
        {
            return View();
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(int id)
        {
            var activity = _activityRepository.Get(id);

            var gpsDataList = XmlFileReader(activity.FilePath);


            var locationViewModel = new LocationViewModel();
            locationViewModel.GpsDatas = gpsDataList;
            locationViewModel.Distance = activity.Distance;
            locationViewModel.Speed = activity.Speed;
            locationViewModel.Time = activity.Duration;
            locationViewModel.BurnedCalorie = activity.Calorie;

            ViewBag.ActivityId = id;
            ViewBag.ActivityType = activity.ActivityType.Title;
            ViewBag.UserProfileId = activity.UserProfileId;
            return View("~/Views/Location/CurrentActivity.cshtml", locationViewModel);
        }

        public ActionResult CreateActivity()
        {
            var activities = _activityTypeRepository.GetActivityTypes();
            return View(activities);
        }

        [HttpPost]
        public ActionResult CurrentActivity(HttpPostedFileBase file, int activityTypeId)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/gps File/"), fileName);
                file.SaveAs(path);

                var gpsDataList = XmlFileReader(fileName);


                var distance = CalculateDistance(gpsDataList);
                var finalDistance = Math.Round(distance, 2);

                var timeStamp = Math.Round((gpsDataList.Last().Time - gpsDataList.First().Time).TotalHours, 2);
                var avgSpeed = Math.Round(distance/timeStamp, 2);

                var locationViewModel = new LocationViewModel();
                locationViewModel.GpsDatas = gpsDataList;
                locationViewModel.Distance = finalDistance;
                locationViewModel.Speed = avgSpeed;
                locationViewModel.Time = timeStamp;


                //var user = userRepository.GetUserByProfileId(WebSecurity.CurrentUserId);
                ////var user = userRepository.GetUserByProfileId(intuserID);
                //var weight = Convert.ToDouble(user.Weight);
                //var met = _metConditionRepository.GetMET(avgSpeed, activityTypeId);
                // var cal = Math.Round(CalculateCalorie( met,weight, timeStamp),2);
                var cal = Math.Round(CalculateCalorie(activityTypeId, avgSpeed, timeStamp), 2);
                if (cal == 0.0)
                {
                    return RedirectToAction("Create", "User");
                }

                locationViewModel.BurnedCalorie = cal;

                //@ViewBag.Fabiha = locationViewModel;
                var activity = new Activity
                {
                    Distance = finalDistance,
                    Speed = avgSpeed,
                    Calorie = cal,
                    Duration = timeStamp,
                    CreatedOn = DateTime.Now,
                    UserProfileId = WebSecurity.CurrentUserId,
                    FilePath = fileName,
                    ActivityTypeId = activityTypeId

                };
                var isSuccess = _activityRepository.Add(activity);
                ViewBag.ActivityId = activity.Id;
                ViewBag.ActivityType = _activityTypeRepository.Get(activityTypeId).Title;
                ViewBag.UserProfileId = activity.UserProfileId;
                return View(locationViewModel);
            }

            ViewBag.ErrorMessage = "Please Upload a Activity.gpx format File";
            return View("CustomError");
        }

        private double CalculateDistance(List<GpsData> gpsDataList)
        {
            double distance = 0;
            for (int i = 0; i < gpsDataList.Count - 1; i++)
            {
                if (i + 1 == gpsDataList.Count - 1) break;
                var sLat = double.Parse(gpsDataList.ElementAt(i).Latitude);
                var sLng = double.Parse(gpsDataList.ElementAt(i).Longitude);
                var eLat = double.Parse(gpsDataList.ElementAt(i + 1).Latitude);
                var eLng = double.Parse(gpsDataList.ElementAt(i + 1).Longitude);
                distance = distance + Calc(sLat, sLng, eLat, eLng);
            }
            return distance;
        }

        private List<GpsData> XmlFileReader(string fileName)
        {
            var gpsDataList = new List<GpsData>();
            var xmldoc = new XmlDocument();
            xmldoc.Load(HttpContext.Server.MapPath("~/Content/gps File/" + fileName));
            XmlNodeList trackPointNodes = xmldoc.GetElementsByTagName("trkpt");
            foreach (XmlNode node in trackPointNodes)
            {
                if (node.Attributes != null)
                {
                    string lat = node.Attributes["lat"].Value;
                    var lng = node.Attributes["lon"].Value;
                    var ele = node.ChildNodes[0].InnerText;
                    var time = node.ChildNodes[1].InnerText;
                    var a = DateTime.Now;

                    gpsDataList.Add(new GpsData
                    {
                        Latitude = lat,
                        Longitude = lng,
                        Elevation = ele,
                        Time = DateTime.Parse(time, null, DateTimeStyles.RoundtripKind)
                        //Call the DateTime.Parse(String, IFormatProvider, DateTimeStyles) method,
                        //and pass DateTimeStyles.RoundtripKind as the value of the styles parameter.
                    });
                }
            }
            return gpsDataList;
        }

        public ActionResult MyActivities()
        {
            var activities = _activityRepository.GetMyActivities();
            return View(activities);
        }

        public ActionResult Statistics(int id)
        {
            //var activity = _activityRepository.Get(id);
            //var gpsDataList = XmlFileReader(activity.FilePath);
            //var copyGpsDataList = gpsDataList;

            //var distanceListVersusTime = new List<double>();
            //var duration = 0;
            //int startIndex = 0;

            //for (int i = startIndex; i < copyGpsDataList.Count; i++)
            //{
            //    var initialTime = copyGpsDataList[startIndex].Time;
            //    duration = Convert.ToInt32((gpsDataList[i].Time - initialTime).TotalHours);
            //    if (duration == 1)
            //    {
            //        var d = CalculateDistance(copyGpsDataList.GetRange(startIndex, i-startIndex));
            //        distanceListVersusTime.Add(d);
            //        startIndex = i + 1;
            //    }
            //}

            ViewBag.Id = id;
            return View();
        }

        public ActionResult GetDistanceVersusTimeGraphData(int id)
        {

            var activity = _activityRepository.Get(id);
            var gpsDataList = XmlFileReader(activity.FilePath);
            var copyGpsDataList = gpsDataList;

            var distanceListVersusTime = new List<double>();
            var duration = 0;
            int startIndex = 0;

            for (int i = startIndex; i < copyGpsDataList.Count; i++)
            {
                var initialTime = copyGpsDataList[startIndex].Time;
                duration = Convert.ToInt32((gpsDataList[i].Time - initialTime).TotalHours);
                if (duration == 1)
                {
                    var d = Math.Round(CalculateDistance(copyGpsDataList.GetRange(startIndex, i - startIndex)), 2);
                    distanceListVersusTime.Add(d);
                    startIndex = i + 1;
                }
            }
            return Json(distanceListVersusTime, JsonRequestBehavior.AllowGet);
        }

        public double GetDistanceFromTwoGpsData(GpsData start, GpsData end)
        {
            var sLat = double.Parse(start.Latitude);
            var sLng = double.Parse(start.Longitude);
            var eLat = double.Parse(end.Latitude);
            var eLng = double.Parse(end.Longitude);
            return Calc(sLat, sLng, eLat, eLng);
        }

        public ActionResult GetSpeedVersusDistanceGraphData(int id)
        {
            var activity = _activityRepository.Get(id);
            var gpsDataList = XmlFileReader(activity.FilePath);
            var copyGpsDataList = gpsDataList;

            var speedListVersusTime = new List<double>();
            var duration = 0;
            int startIndex = 0;
            double distance = 0;
            for (int i = startIndex; i < copyGpsDataList.Count; i++)
            {
                if (i + 1 == copyGpsDataList.Count - 1) break;
                distance = distance + GetDistanceFromTwoGpsData(copyGpsDataList[i], copyGpsDataList[i + 1]);
                if (distance >= 1)
                {
                    var time = Math.Round((copyGpsDataList[i].Time - copyGpsDataList[startIndex].Time).TotalHours, 3);
                    speedListVersusTime.Add(time);
                    startIndex = i + 1;
                    distance = 0;
                }
            }
            return Json(speedListVersusTime, JsonRequestBehavior.AllowGet);
        }


        //public double CalculateCalorie(double met, double weight, double duration)
        //{
        //    var burnedCal = met*weight*duration;
        //    return burnedCal;
        //}
        public double CalculateCalorie(int activityTypeId, double speed, double duration)
        {
            var burnedCal=0.0;
            var user = userRepository.GetUserByProfileId(WebSecurity.CurrentUserId);
            //var user = userRepository.GetUserByProfileId(intuserID);
            if (user == null)
            {
                return burnedCal;
            }
            var weight = Convert.ToDouble(user.Weight);
            var met = _metConditionRepository.GetMET(speed, activityTypeId);
            burnedCal = met*weight*duration;
            return burnedCal;
        }

        public ActionResult GetElevetionVersusdistanceGraphData(int id)
        {

            var activity = _activityRepository.Get(id);
            var gpsDataList = XmlFileReader(activity.FilePath);
            var copyGpsDataList = gpsDataList;
            var listOfEle = ConverStringEleToDouble(gpsDataList);
            var elevationListVersusDistance = new List<double>();
            // var duration = 0;
            int startIndex = 0;
            double distance = 0;
            double elevation = 0;
            for (int i = startIndex; i < copyGpsDataList.Count; i++)
            {
                if (i + 1 == copyGpsDataList.Count - 1) break;

                elevation = elevation + listOfEle[i];
                distance = distance + GetDistanceFromTwoGpsData(copyGpsDataList[i], copyGpsDataList[i + 1]);

                if (distance >= 1)
                {
                    elevation = Math.Round(elevation/(i - startIndex), 2);
                    elevationListVersusDistance.Add(elevation);
                    startIndex = i + 1;
                    distance = 0;
                }
            }
            return Json(elevationListVersusDistance, JsonRequestBehavior.AllowGet);
        }

        public List<double> ConverStringEleToDouble(List<GpsData> gpsDataList)
        {
            var listOfEleInDouble = new List<double>();
            for (int i = 0; i < gpsDataList.Count - 1; i++)
            {
                var ele = double.Parse(gpsDataList.ElementAt(i).Elevation);
                listOfEleInDouble.Add(ele);
            }
            return listOfEleInDouble;
        }

        public ActionResult DeleteActivity(int id)
        {
            _activityRepository.DeleteActivity(id);
            return RedirectToAction("MyActivities");
        }

        public ActionResult OverallStatistics()
        {
            return View();
        }

        public ActionResult FromDayToDayStatistics(int numberOfDays = 0)
        {
            var activities = _activityRepository.GetDataFromDateRange(numberOfDays);
            return Json(activities, JsonRequestBehavior.AllowGet);
        }
    }

   

}
