using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class UserGroupRelationRepository
    {
        SportsTrackerContext db = null;

        public UserGroupRelationRepository()
        {
            db = new SportsTrackerContext();
        }

        public bool Add(UserGroupRelation groupMember)
        {
            db.UserGroupRelations.Add(groupMember);
            return db.SaveChanges() > 0;
        }

        public ISet<User> MemberListByGroupId(int id)
        {
           // string query = "SELECT * FROM User,UserGroupRelation WHERE User.UserProfileId=UserGroupRelation.UserId";
            var user = from usr in db.Users
                join member in db.UserGroupRelations on usr.UserProfileId equals member.UserId
                where (member.Groupid == id)
                select usr;
            user.ToList();
            ISet<User> u = new HashSet<User>(user);

            return u ;
        }

        public List<Post> groupPostList(int id)
        {
            return db.Posts.Where(p => p.GroupId == id).ToList();

        } 

        
    }
}