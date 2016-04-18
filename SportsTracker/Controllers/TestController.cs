using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SportsTracker.Models;

namespace SportsTracker.Controllers
{
    public class TestController : Controller
    {
        ////
        //// GET: /Test/
        //[System.Web.Mvc.HttpPost]
        //public ActionResult Index(dynamic obj)
        //{
        //    using (StreamWriter writer = new StreamWriter("C:\\log.txt", true))
        //    {
        //        writer.WriteLine(obj.ToString() + obj.VehiceId);
        //    }
        //    return null;
        //}


        public ActionResult GetInfoForCalorie()
        {

            return View();
        }

        public ActionResult CalculateCalorie(Calorie cal)
        {
            var MET = 0.0;

            if (cal.Sports == "Cycling")
            {
                MET = 8.5;
            }
            else if (cal.Sports == "Running")
            {
                MET = 13.3;
            }
            else
            {
                MET = 7.0;
            }

            var burnedCal = MET * cal.Weight * cal.Distance *(1/cal.Duration);

            return View(burnedCal);
        }
    }
}
