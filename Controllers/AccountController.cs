using ProjectFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectFirst.Models.ViewModel;

namespace ProjectFirst.Controllers
{
    public class AccountController : Controller
    {
        ProjectEntities _db = new ProjectEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
            using (ProjectEntities db = new ProjectEntities())
            {
                var users = db.tblUsers.Where(a => a.Username == l.Username && a.Password == l.Password).FirstOrDefault();
                if (users != null)
                {
                    Session.Add("username", users.Username);
                    FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            }
            return View();

        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel ch)
        {
            string username = Session["username"].ToString();

            tblUser us = _db.tblUsers.Where(u => u.Username == username && u.Password == ch.OldPassword).FirstOrDefault();
            if (us != null)
            {
                us.Password = ch.NewPassword;
                _db.SaveChanges();

            }
            else
            {
                return Json(new { success = false, message = "You Enter Wrong Password" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, message = "Password Changed Successfully" }, JsonRequestBehavior.AllowGet);
        }
      
    }
}
