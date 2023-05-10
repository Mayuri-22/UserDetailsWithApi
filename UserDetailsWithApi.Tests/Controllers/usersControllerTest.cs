using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UserDetailsWithApi;
using UserDetailsWithApi.Controllers;
using UserDetailsWithApi.Models;

namespace UserDetailsWithApi.Tests.Controllers
{
    [TestClass]
    public class usersControllerTest
    {
        [TestClass]
        public class _usersControllerTest
        {
            readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];
            readonly string token = ConfigurationManager.AppSettings["token"];
            [TestMethod]
            public void _GetUsersList()
            {
                usersController controller = new usersController();
                var Result = controller.GetUserList(apiBaseAddress, token);
                Assert.IsNotNull(Result);
            }

            [TestMethod]
            public void _AddUserTrue()
            {
                Users users = new Users();
                users.name = "krishna";
                users.email = $"K{DateTime.Now.ToString("ddMMyyyyHHmmss")}@test.com";
                users.gender = "Male";
                users.status = "ACTIVE";
                usersController controller = new usersController();
                var Result = controller.addUsers(users, apiBaseAddress, token);
                Assert.IsTrue(Result, "ADD TEST PASSED");
            }

            [TestMethod]
            public void _AddUserFalse()
            {
                Users users = new Users();
                users.name = "test";
                users.email = "test@test.com";
                users.gender = "Male";
                users.status = "test";
                usersController controller = new usersController();
                var Result = controller.addUsers(users, apiBaseAddress, token);
                Assert.IsFalse(Result, "ADD TEST FAILED");
            }


            [TestMethod]
            public void _UpdateUserTrue()
            {
                usersController controller = new usersController();
                var UserList = controller.GetUserList(apiBaseAddress, token);
                if (UserList.Count > 0)
                {
                    Users users = new Users();
                    users.name = "jasmin";
                    users.email = UserList[0].email;
                    users.gender = "FeMale";
                    users.status = "Active";
                    users.id = UserList[0].id;
                    var Result = controller.updateUsers(users, apiBaseAddress, token);
                    Assert.IsTrue(Result, "Update Test Passs ");
                }
            }
            [TestMethod]
            public void _UpdateUserFalse()
            {
                usersController controller = new usersController();
                Users users = new Users();
                users.name = "jasmin";
                users.email = "j@test.com";
                users.gender = "FeMale";
                users.status = "Active";
                users.id = 0;
                var Result = controller.updateUsers(users, apiBaseAddress, token);
                Assert.IsFalse(Result, "Update Test Failed ");
            }

            [TestMethod]
            public void _DeleteUserTrue()
            {
                usersController controller = new usersController();
                var UserList = controller.GetUserList(apiBaseAddress, token);
                if (UserList.Count > 0)
                {
                    string id = UserList[0].id.ToString();
                    var Result = controller.deleteUsers(id, apiBaseAddress, token);
                    Assert.IsTrue(Result, " DELETE TEST PASSED");
                }
            }


            [TestMethod]
            public void _DeleteUserFail()
            {
                
                usersController controller = new usersController();
                string id = "0";
                var Result = controller.deleteUsers(id, apiBaseAddress, token);
                Assert.IsFalse(Result, " DELETE TEST FAILED");
            }
        }
    }
}