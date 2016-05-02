using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.ViewModel
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        public double Distance { get; set; }
        public double Speed { get; set; }
        public double Calorie { get; set; }
        public double Duration { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string FilePath { get; set; }

        public int UserProfileId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityTypeTitle { get; set; }
       
    }
}