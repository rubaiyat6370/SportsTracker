using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.ViewModel
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string Groupname { get; set; }
        public ISet<User> Users { get; set; }
        public List<Post> PostsList { get; set; } 
        public int Admin { get; set; }
    }
}