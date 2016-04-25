using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SportsTracker.Models.ViewModel
{
    public class LocationViewModel
    {
        public List<GpsData> GpsDatas { get; set; }
        public double Distance { get; set; }
        public double Speed { get; set; }
    }
}