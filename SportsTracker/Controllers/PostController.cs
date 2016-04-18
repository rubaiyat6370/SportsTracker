using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTracker.Filters;
using SportsTracker.Models.DbModel;
using SportsTracker.Models.Repository;
using SportsTracker.Models.ViewModel;
using WebMatrix.WebData;

namespace SportsTracker.Controllers
{
    [InitializeSimpleMembership]
    public class PostController : Controller
    {
        readonly PostRepository _postRepository = new PostRepository();
        readonly UserRepository _userRepository = new UserRepository();
        //
        // GET: /Post/

        public ActionResult Index()
        {
            var postviewModels = new List<PostViewModel>();
            var posts = _postRepository.GetLatestPosts(10).ToList();
            foreach (var post in posts)
            {
                SimpleMembershipProvider provider = new SimpleMembershipProvider();
                postviewModels.Add(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.PostDescription,
                    User = _userRepository.GetUserById(post.UserId),
                    CreatedOn = post.CreatedOn
                });
            }
            return View();
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(int id)
        {
            Post post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Post/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (WebSecurity.CurrentUserId > 0)
            {
                post.UserId = WebSecurity.CurrentUserId;
                post.CreatedOn = DateTime.Now;
                _postRepository.AddPost(post);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Post/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var postReository = new PostRepository();
            var post = postReository.GetPostById(id);
            return View(post);
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            var postReository = new PostRepository();
            if (WebSecurity.CurrentUserId > 0)
            {
                post.UserId = WebSecurity.CurrentUserId;
                if (postReository.EditPost(post))
                {
                    return RedirectToAction("Index");
                }
                return View(post);
            }
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Post/Delete/5

        public ActionResult Delete(int id )
        {
            Post post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Post/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = _postRepository.GetPostById(id);
            if (WebSecurity.CurrentUserId > 0)
            {
                if (_postRepository.DeletePost(post))
                {
                    return RedirectToAction("Index");
                }
                return View(post);
            }
            return RedirectToAction("Login", "Account");
        }

      
    }
}