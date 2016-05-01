using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.DbModel
{
    public class SportsTrackerContext : DbContext
    {
        public SportsTrackerContext()
            : base("SportsTrackerConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroupRelation> UserGroupRelations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<METCondition> MetConditions { get; set; }
    }
}