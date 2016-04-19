using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public int UserProfileId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
    }
}