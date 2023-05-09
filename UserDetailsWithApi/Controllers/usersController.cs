using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserDetailsWithApi.Models;

namespace UserDetailsWithApi.Controllers
{
    public class usersController : Controller
    {
        // GET: users
        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];
        readonly string token = ConfigurationManager.AppSettings["token"];
        static List<Users> lstUsers = new List<Users>();
        UserRepository userRepository = new UserRepository();
        /// <summary>
        /// This method is return all users details
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Users> lstuser = new List<Users>();
            lstuser = GetUserList(apiBaseAddress, token);
            lstUsers = lstuser;
            return View(lstuser);
        }
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// This Method is used to add user.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                bool result = addUsers(users, apiBaseAddress, token);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Duplicate Email Address Or Server Error.";
                }
            }
            return View(users);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = new Users();
            user = lstUsers.FirstOrDefault(u => u.id == Convert.ToInt32(id));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        /// <summary>
        /// This method is used to update the user .
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Users users)
        {
            if (ModelState.IsValid)
            {
                bool result = updateUsers(users, apiBaseAddress, token);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Record Not Found Or Server Error";
                }
            }
            return View(users);
        }
        /// <summary>
        /// This method is used to remove the particular user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            bool result = deleteUsers(id, apiBaseAddress, token);
            if (result)
                ModelState.AddModelError("Success", "Delete Sucessfully");
            else
            {
                ModelState.AddModelError("Error", "Delete Failed");
                ViewBag.Message = "User Delete Failed or Server Error";
            }
            // return Json(null);
            return RedirectToAction("Index");

        }
        public List<Users> GetUserList(string ApiAddress, string token)
        {
            List<Users> lstuser = new List<Users>();
            string Result = userRepository.getUserlist(ApiAddress, token);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                lstuser = JsonConvert.DeserializeObject<List<Users>>(Result);
            }
            return lstuser;
        }
        public bool addUsers(Users users, string apiBaseAddress, string token)
        {
            string json = JsonConvert.SerializeObject(users);
            bool result = userRepository.addUser(json, apiBaseAddress, token);
            if (result)
            {
                return true;
            }
            return false;
        }
        public bool updateUsers(Users users, string apiBaseAddress, string token)
        {
            string User = JsonConvert.SerializeObject(users);
            bool result = userRepository.updateUser(users.id, User, apiBaseAddress, token);
            if (result)
                return true;
            return false;
        }
        public bool deleteUsers(string id, string apiBaseAddress, string token)
        {
            bool result = userRepository.RemoveUser(Convert.ToInt32(id), apiBaseAddress, token);
            if (result)
                return true;
            return false;
        }
    }
}