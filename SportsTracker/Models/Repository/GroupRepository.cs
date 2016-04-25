using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;
using WebMatrix.WebData;

namespace SportsTracker.Models.Repository
{
    
    public class GroupRepository
    {
        public SportsTrackerContext _db = null;

        public GroupRepository()
        {
            _db = new SportsTrackerContext();
        }

        //insert into User(userName) values ('a')
        public bool AddGroup(Group group)
        {
            _db.Groups.Add(group);
            return _db.SaveChanges() > 0;
        }

        //update user set userName = 'a' where id = 1
        public bool EditGroup(Group group)
        {
            _db.Groups.Attach(group);
            _db.Entry(group).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        //delete from User where id = 1
        public bool DeleteGroup(Group group)
        {
            _db.Groups.Attach(group);
            _db.Entry(group).State = EntityState.Deleted;
            return _db.SaveChanges() > 0;
        }

        //select * from user
        public List<Group> GetGroupsList()
        {
            return _db.Groups.ToList();
        }

        //select * from user where id = 1
        public Group GetGroupsById(int groupId)
        {
            return _db.Groups.FirstOrDefault(g => g.Id == groupId);
        }
    }
}