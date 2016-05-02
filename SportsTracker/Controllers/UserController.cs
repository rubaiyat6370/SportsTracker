using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsTracker.Models.DbModel;
using SportsTracker.Models.Repository;
using WebMatrix.WebData;

namespace SportsTracker.Controllers
{
    public class UserController : Controller
    {
        UserRepository _userRepository = new UserRepository();

        public ActionResult SearchUser(string searchBy, string search)
        {
            List<User> users = new List<User>();
            users = _userRepository.SearchUser(searchBy, search);
            return Json(users, JsonRequestBehavior.AllowGet);
            //return View(users);
        }

        //
        // GET: /User/Details/5

        

        //
        // GET: /User/Create
        
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
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
