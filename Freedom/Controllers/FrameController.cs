using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Freedom.Models;
namespace Freedom.Controllers
{
    public class FrameController :BaseController
    {
        //
        // GET: /Frame/
        public ActionResult Index()
        {
            var users = (from u in db.Userprofile
                         where u.UserID == WebSecurity.CurrentUserId
                         select u).First();
            return View(users);
        }
        public ActionResult Info()
        {
            string username = WebSecurity.CurrentUserName;
            var user = (from u in db.Userprofile
                        where u.UserName == username
                        select u).First();
            var order = (from u in db.Order_Detail
                         where u.AdminID == user.UserID
                         select u);
            ViewData["order"] = order;
            return View(user);
        }
        public ActionResult Calender()
        {
            return View();
        }
        public ActionResult Reservation()
        {
            return View();
        }

        public ActionResult ReservationByEarth()
        {
            return View();
        }
        public ActionResult HotActivity()
        {
            return View();
        }
        public ActionResult SetPassword()
        {
            return View();
        }  

    }
}
