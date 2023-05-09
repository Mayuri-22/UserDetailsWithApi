using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace UserDetailsWithApi.Models
{
    public class Users
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string email { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Enter Gender")]
        public string gender { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please Enter Status ")]
        public string status { get; set; }
    }
}