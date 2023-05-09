using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserDetailsWithApi.Models
{
    internal interface IUserRepository
    {
        /// <summary>
        /// Create four method that defind in User
        /// </summary>
        /// <returns></returns>
        string getUserlist(string apiBaseAddress, string token);
        bool addUser(string user, string apiBaseAddress, string token);
        bool updateUser(int id, string user, string apiBaseAddress, string token);

        bool RemoveUser(int id, string apiBaseAddress, string token);

    }
}