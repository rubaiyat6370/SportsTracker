using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class UserRepository
    {
        private SportsTrackerContext db = null;

        public UserRepository()
        {
            db = new SportsTrackerContext();
        }

        //insert into User(userName) values ('a')
        public bool AddUser(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        //update user set userName = 'a' where id = 1
        public bool EditUser(User user)
        {
            db.Users.Attach(user);
            db.Entry(user).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        //delete from User where id = 1
        public bool DeleteUser(User user)
        {
            db.Users.Attach(user);
            db.Entry(user).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        //select * from user
        public List<User> GetUsersList()
        {
            return db.Users.ToList();
        }

        //select * from user where id = 1
        public User GetUserById(int userId)
        {
            return db.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}