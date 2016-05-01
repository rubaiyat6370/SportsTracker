using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using SportsTracker.Models.DbModel;
using WebMatrix.WebData;

namespace SportsTracker.Models.Repository
{
    public class PostRepository
    {
        public SportsTrackerContext _db = null;

        public PostRepository()
        {
            _db = new SportsTrackerContext();
        }

        public bool AddPost(Post post)
        {
            _db.Posts.Add(post);
            return _db.SaveChanges() > 0;
        }

        //update user set userName = 'a' where id = 1
        public bool EditPost(Post post)
        {
            
            _db.Posts.Attach(post);
            _db.Entry(post).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        //delete from User where id = 1
        public bool DeletePost(Post post)
        {
            _db.Posts.Attach(post);
            _db.Entry(post).State = EntityState.Deleted;
            return _db.SaveChanges() > 0;
        }

        //select * from user
        public List<Post> GetPostsQuery()
        {
            return _db.Posts.ToList();
        }

        public List<Post> GetMyPosts()
        {
            //return _db.Posts.Where(p => p.UserId == WebSecurity.CurrentUserId ).ToList();
            var posts = from p in _db.Posts.Where(p => p.UserId == WebSecurity.CurrentUserId)
                orderby p.CreatedOn descending
                select p;
            return posts.ToList();
        } 

        public IQueryable<Post> GetLatestPosts(int numberOfPost)
        {
            var posts = from p in _db.Posts.Where(p=>p.GroupId==0) 
                        orderby p.CreatedOn descending
                        select p;

            return posts;
        }

        //select * from user where id = 1
        public Post GetPostById(int postId)
        {
            return _db.Posts.FirstOrDefault(u => u.Id == postId);
        }
    
    }
}