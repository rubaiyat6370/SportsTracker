using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("SetGoal")]
    public class SetGoal
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public double Distance { get; set; }
        public double Calorie { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EndOn { get; set; }
        public bool IsAchieved { get; set; }
        public bool IsComplete { get; set; }
    }
}