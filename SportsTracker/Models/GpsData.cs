using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTracker.Models
{
    public class GpsData
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Elevation { get; set; }
        public DateTime Time { get; set; }
    }
}