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

        public List<User> SearchUser(string searchBy, string search)
        {
            List<User> users = new List<User>();
            if (searchBy == "Email")
                users = db.Users.Where(x => x.Email.StartsWith(search)).ToList();
            else if (searchBy=="Username")
            {
                users = db.Users.Where(x => x.UserName.StartsWith(search)).ToList();
            }
            else if (searchBy == "Lastname")
            {
                users = db.Users.Where(x => x.Lastname.StartsWith(search)).ToList();
            }

            else users = db.Users.Where(x => x.Firstname.StartsWith(search)).ToList();

            return users;
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
            return db.Users.SingleOrDefault(u => u.UserProfileId == userId);
        }

        public User GetUserByProfileId(int userProfileId)
        {
            return db.Users.SingleOrDefault(u => u.UserProfileId == userProfileId);
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    