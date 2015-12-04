using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Freedom.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
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
