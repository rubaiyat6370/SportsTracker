using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("Activity")]
    public class Activity
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

        public virtual ActivityType ActivityType { get; set; }
    }
}