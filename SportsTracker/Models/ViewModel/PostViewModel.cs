using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int UserId { get; set; }
        //public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public User User { get; set; }
    }
}