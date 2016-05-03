using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
    public class GroupController : Controller
    {
        readonly PostRepository _postRepository = new PostRepository();
        readonly UserRepository _userRepository = new UserRepository();
        GroupRepository _groupRepository = new GroupRepository();
        UserGroupRelationRepository _groupRelationRepository = new UserGroupRelationRepository();

        private List<GroupViewModel> groupViewModels = null;
        private List<Group> groups = null;
        private List<User> users = null;
        private List<Post> posts = null; 
        private UserGroupRelation groupMember = null; 

        public ActionResult Index()
        {

            var groups = _groupRepository.GetGroupsList();

            return View(groups);
        }

        public ActionResult MyGroups()
        {
            
            var myGroups = _groupRepository.GetMyGroups();

            return View(myGroups);
        }

        //
        // GET: /Group/Details/5

        public ActionResult Details(int id)
        {
           
            Group group = _groupRepository.GetGroupById(id);
            GroupViewModel groupViewModel = new GroupViewModel();
            User user = new User();
            //groups = _groupRepository.GetGroupsList();
            //users = _userRepository.GetUsersList();
            //posts = _postRepository.GetPostsQuery();
            ViewBag.User = _groupRelationRepository.isExist(id, WebSecurity.CurrentUserId);
            

            groupViewModel.GroupId = group.Id;
            groupViewModel.Groupname = group.Groupname;
            groupViewModel.Admin = group.UserId;
            groupViewModel.Users = _groupRelationRepository.MemberListByGroupId(id);
            groupViewModel.PostsList = _groupRelationRepository.GroupPostList(id);
            

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(groupViewModel);
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group)
        {
            if (WebSecurity.CurrentUserId > 0)
            {
                UserGroupRelation groupMember = new UserGroupRelation();

                group.UserId = WebSecurity.CurrentUserId;
                _groupRepository.AddGroup(group);
                ViewBag.GroupId = group.Id;
                
                groupMember.UserId = WebSecurity.CurrentUserId;
                groupMember.Groupid = group.Id;
                _groupRelationRepository.Add(groupMember);


                return RedirectToAction("AddMember", new { groupId = group.Id });
                
            }
            return RedirectToAction("Login", "Account");

        }

        public ActionResult CreatePost(int id)
        {
            ViewBag.groupId =id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(Post post)
        {

            post.UserId = WebSecurity.CurrentUserId;
            post.CreatedOn = DateTime.Now;
            _postRepository.AddPost(post);
            return RedirectToAction("Details", new {id=post.GroupId});
        }


        //
        // GET: /Group/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_groupRepository.GetGroupById(id));
        }

        //
        // POST: /Group/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Group group)
        {
            group.UserId = WebSecurity.CurrentUserId;
            if(_groupRepository.EditGroup(group))
            {
                return RedirectToAction("MyGroups");
            }
            return View(group);
        }

        //
        // GET: /Group/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Group/Delete/5

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

        //
        // Add user to a group
        public ActionResult AddMember(int groupId)
        {
            ViewBag.GroupId = groupId;
            return View(users);
        }

        public ActionResult AddUserToGroup(int groupId, int userId)
        {
           
            _groupRelationRepository.Add(new UserGroupRelation
            {
                Groupid = groupId,
                UserId = userId
            });
            return RedirectToAction("AddMember", new {groupId = groupId});
        }

        public ActionResult RemoveMember( int groupId, int userId)
        {
            _groupRelationRepository.DeleteMember(groupId, userId);
            return RedirectToAction("Details", new { id = groupId });
        }

    }
}
