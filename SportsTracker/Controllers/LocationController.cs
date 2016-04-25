using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SportsTracker.Models;
using SportsTracker.Models.ViewModel;

namespace SportsTracker.Controllers
{

    public class LocationController : Controller
    {
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
            for (int i = 0; i < gpsDataList.Count-1; i++)
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
            
            //@ViewBag.Fabiha = locationViewModel;
            return View(locationViewModel);
        }


        public static double Calc(double lat1,
                      double long1, double lat2, double long2)
        {
           
            double dDistance = Double.MinValue;
            double dLat1InRad = lat1 * (Math.PI / 180.0);
            double dLong1InRad = long1 * (Math.PI / 180.0);
            double dLat2InRad = lat2 * (Math.PI / 180.0);
            double dLong2InRad = long2 * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Asin(Math.Sqrt(a));

            //c = 2 * atan2(sqrt(a), sqrt(1-a)) 
            //double c = 2.0 * Math.Atan2(Math.Pow(a, 2), Math.Pow((1 - a), 2));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms * c;

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
            return View();
        }

       
    }
}
