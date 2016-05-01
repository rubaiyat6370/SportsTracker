using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTracker.Models.DbModel;
using WebMatrix.WebData;

namespace SportsTracker.Models.Repository
{
    public class GroupRepository
    {
        public SportsTrackerContext db = null;

        public GroupRepository()
        {
            db = new SportsTrackerContext();
        }

        //insert into User(userName) values ('a')
        public bool AddGroup(Group group)
        {
            db.Groups.Add(group);
            return db.SaveChanges() > 0;
        }

        //update user set userName = 'a' where id = 1
        public bool EditGroup(Group group)
        {
            db.Groups.Attach(group);
            db.Entry(group).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        //delete from User where id = 1
        public bool DeleteGroup(Group group)
        {
            db.Groups.Attach(group);
            db.Entry(group).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        //select * from user
        public ISet<Group> GetGroupsList()
        {
            var groups = db.Groups.ToList();
            ISet<Group> groupSet = new HashSet<Group>(groups);

            return groupSet;
        }

        public ISet<Group> GetMyGroups()
        {
            var groups = from g in db.Groups
                       join member in db.UserGroupRelations on g.Id equals member.Groupid
                       where (member.UserId == WebSecurity.CurrentUserId)
                       select g;
            var list = groups.ToList();
            ISet<Group> grpSet = new HashSet<Group>(list);
            return grpSet;

            //return db.Groups.Where(p => p.UserId == WebSecurity.CurrentUserId).ToList();
        } 

        //select * from user where id = 1
        public Group GetGroupById(int groupId)
        {
            return db.Groups.SingleOrDefault(u => u.Id == groupId);
        }

        public List<Group> SearchGroup(string searchBy, string search)
        {
            List<Group> groups = new List<Group>();
            if (searchBy=="Groupname")
            {
                groups = db.Groups.Where(x => x.Groupname.StartsWith(search)).ToList();
            }
            groups = db.Groups.Where(x => x.Groupname.StartsWith(search)).ToList();
            return groups;
        }

        

       

    }
}