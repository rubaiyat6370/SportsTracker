using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class UserGroupRelationRepository
    {
        SportsTrackerContext _db = null;

        public UserGroupRelationRepository()
        {
            _db = new SportsTrackerContext();
        }

        public bool Add(UserGroupRelation groupMember)
        {
            _db.UserGroupRelations.Add(groupMember);
            return _db.SaveChanges() > 0;
        }

        public ISet<User> MemberListByGroupId(int id)
        {
           // string query = "SELECT * FROM User,UserGroupRelation WHERE User.UserProfileId=UserGroupRelation.UserId";
            var user = from usr in _db.Users
                join member in _db.UserGroupRelations on usr.UserProfileId equals member.UserId
                where (member.Groupid == id)
                select usr;
            user.ToList();
            ISet<User> u = new HashSet<User>(user);

            return u ;
        }

        public List<Post> GroupPostList(int id)
        {
            return _db.Posts.Where(p => p.GroupId == id).ToList();

        }

        public bool DeleteMember(int groupId, int userId)
        {
            var u = from ur in _db.UserGroupRelations
                where (ur.Groupid == groupId && ur.UserId == userId)
                select ur;
            foreach (var userGroupRelation in u)
            {
                _db.UserGroupRelations.Remove(userGroupRelation);
            }
            return _db.SaveChanges() > 0;
        }

        
    }
}