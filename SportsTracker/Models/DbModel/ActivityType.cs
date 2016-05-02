using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("ActivityType")]
    public class ActivityType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<METCondition> MetConditions { get; set; }

    }
}