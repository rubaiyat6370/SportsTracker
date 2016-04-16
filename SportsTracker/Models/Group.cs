using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models
{
    [Table("Group")]
    public class Group
    {
        public int Id { get; set; }
        public string Groupname { get; set; }
        public int UserId { get; set; }
    }
}