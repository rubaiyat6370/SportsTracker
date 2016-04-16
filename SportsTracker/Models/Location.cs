using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models
{
    [Table("Location")]
    public class Location
    {
        public int Id { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }
    }
}