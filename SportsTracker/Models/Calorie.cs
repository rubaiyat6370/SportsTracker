using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTracker.Models
{
    public class Calorie
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Age { get; set; }
        public String Gender { get; set; }
        public string Sports { get; set; }
        public double Duration { get; set; }
        public double Distance { get; set; }
    }
}