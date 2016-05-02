using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("METCondition")]
    public class METCondition
    {
        public int Id { get; set; }
        public int StartLimit { get; set; }
        public int EndLimit { get; set; }
        public double MET { get; set; }
        public int ActivityTypeId { get; set; }

        public virtual ActivityType ActivityType { get; set; }

    }
}