using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SportsTracker.Controllers
{
    public class TestController : ApiController
    {
        //
        // GET: /Test/
        [System.Web.Mvc.HttpPost]
        public ActionResult Index(dynamic obj)
        {
            using (StreamWriter writer = new StreamWriter("C:\\log.txt", true))
            {
                writer.WriteLine(obj.ToString() + obj.VehiceId);
            }
            return null;
        }

    }
}
