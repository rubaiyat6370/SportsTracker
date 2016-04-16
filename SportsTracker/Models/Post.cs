using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models
{
    [Table("Post")]
    public class Post
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string PostDescript { get; set; }
        public int UserId { get; set; }
    }
}