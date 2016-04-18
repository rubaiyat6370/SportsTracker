using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SportsTracker.Models.DbModel;

namespace SportsTracker.Models.Repository
{
    public class PostRepository
    {
        public SportsTrackerContext sportsTracker = null;

        public PostRepository()
        {
            sportsTracker = new SportsTrackerContext();
        }

        public bool AddUser(Post post)
        {
            sportsTracker.Posts.Add(post);
            /*int returnResult = onlineBazarEntities.SaveChanges();
            return returnResult > 0;*/
            return sportsTracker.SaveChanges() > 0;
        }

        //update user set userName = 'a' where id = 1
        public bool EditUser(Post post)
        {
            sportsTracker.Posts.Attach(post);
            sportsTracker.Entry(post).State = EntityState.Modified;
            return sportsTracker.SaveChanges() > 0;
        }

        //delete from User where id = 1
        public bool DeleteUser(Post post)
        {
            sportsTracker.Posts.Attach(post);
            sportsTracker.Entry(post).State = EntityState.Deleted;
            return sportsTracker.SaveChanges() > 0;
        }

        //select * from user
        public List<Post> GetPostsList()
        {
            return sportsTracker.Posts.ToList();
        }

        //select * from user where id = 1
        public Post GetPostById(int postId)
        {
            return sportsTracker.Posts.FirstOrDefault(u => u.Id == postId);
        }
    
    }
}