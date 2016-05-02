using System;
using System.Collections.Generic;
using System.IO;
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
    public class UserController : Controller
    {
        UserRepository _userRepository = new UserRepository();
        GroupRepository _groupRepository = new GroupRepository();
        PostRepository _postRepository = new PostRepository();
        ActivityRepository _activityRepository = new ActivityRepository();

        public ActionResult SearchUser(string searchBy, string search)
        {
            List<User> users = new List<User>();
            users = _userRepository.SearchUser(searchBy, search);
            return Json(users, JsonRequestBehavior.AllowGet);
            //return View(users);
        }

        public ActionResult Search(string searchBy, string search)
        {
            List<User> users = new List<User>();
            users = _userRepository.SearchUser(searchBy, search);

            return View(users);
        }

        public ActionResult ProfileDetails(int id)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = _userRepository.GetUserById(id);
            profileViewModel.Groups = _groupRepository.GetGroupsByUserId(id);
            profileViewModel.Posts = _postRepository.GetPostsByUserId(id);
            profileViewModel.ActivityList = _activityRepository.GetOtherProfileActivities(id);
            return View(profileViewModel);
        }
        

        //
        // GET: /User/Create
        
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/ProfilePicture"), fileName);
                    file.SaveAs(path);
                    user.ProfilePicturePath = fileName;
                }
                user.UserProfileId = WebSecurity.CurrentUserId;
                user.UserName = WebSecurity.CurrentUserName;
                _userRepository.AddUser(user);

                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details()
        {
            throw new NotImplementedException();
        }

        
    }
}
