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
        private SportsTrackerContext sportsTrackerContext = null;

        public UserRepository()
        {
            sportsTrackerContext = new SportsTrackerContext();
        }

        //insert into User(userName) values ('a')
        public bool AddUser(User user)
        {
            sportsTrackerContext.Users.Add(user);
            /*int returnResult = onlineBazarEntities.SaveChanges();
            return returnResult > 0;*/
            return sportsTrackerContext.SaveChanges() > 0;
        }

        //update user set userName = 'a' where id = 1
        public bool EditUser(User user)
        {
            sportsTrackerContext.Users.Attach(user);
            sportsTrackerContext.Entry(user).State = EntityState.Modified;
            return sportsTrackerContext.SaveChanges() > 0;
        }

        //delete from User where id = 1
        public bool DeleteUser(User user)
        {
            sportsTrackerContext.Users.Attach(user);
            sportsTrackerContext.Entry(user).State = EntityState.Deleted;
            return sportsTrackerContext.SaveChanges() > 0;
        }

        //select * from user
        public List<User> GetUsersList()
        {
            return sportsTrackerContext.Users.ToList();
        }

        //select * from user where id = 1
        public User GetUserById(int userId)
        {
            return sportsTrackerContext.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}