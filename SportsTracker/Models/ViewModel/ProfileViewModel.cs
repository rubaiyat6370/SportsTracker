using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.ViewModel
{
    public class ProfileViewModel
    {
        public ISet<Group> Groups { get; set; }
        public List<Post> Posts { get; set; }
        public List<Activity> ActivityList { get; set; }
        public User User { get; set; }
    }
}