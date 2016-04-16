using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models
{
    [Table("UserGroupRelation")]
    public class UserGroupRelation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Groupid { get; set; }
    }
}