using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("Post")]
    public class Post
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string PostDescription { get; set; }
        public int UserId { get; set; }

    }
}