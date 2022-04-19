using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFirst.Models
{
    public class ChangePasswordViewModel
    {
        ProjectEntities _db = new ProjectEntities();
        public string Username { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Password Mismatch")]
        public string ConfirmNew { get; set; }
    }
}