using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freedom.Models;
using System.Security;
using WebMatrix.WebData;

namespace Freedom.Controllers
{
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Activity(int id)
        {
            TempData["id"] = id;
            return View();
        }
        public ActionResult Date()
        {
            var id = (int)TempData["id"];
            TempData["id"] = id;
            var date = (from p in db.Order_Detail
                        where p.PlaceID == id && p.Condition == "通过"
                        select p);
            List<UserEvent> eventlist = new List<UserEvent>();
            foreach (var item in date)
            {
                UserEvent newEvent = new UserEvent
                {
                    id = item.OrderID,
                    title = null,
                    start = item.UseDate + "T" + item.start,
                    end = item.UseDate + "T" + item.end,
                    color = "red",
                    allDay = false
                };
                eventlist.Add(newEvent);
            }
            var rows = eventlist.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Order()
        {
            Session["date"] = TempData["date"];
            Session["placename"] = TempData["placename"];
            return View();
        }
        [HttpPost]
        public ActionResult SetOrder(Order_Detail date)
        {
            var id = (int)TempData["id"];
            TempData["date"] = date.UseDate;
            var place = (from p in db.Place
                         where p.PlaceID == id
                         select p).First();
            TempData["placename"] = place.PlaceName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(Order_Detail orders)
        {
            if (ModelState.IsValid)
            {
                orders.UseDate = Convert.ToString(Request.Form["usedate"]);
                var PlaceName = Convert.ToString(Request.Form["placename"]);
                var order = (from o in db.Order_Detail
                             select o);
                var placeid = (from p in db.Place
                                 where p.PlaceName == PlaceName
                                 select p.PlaceID).First();
                orders.PlaceID = placeid;
                var start = Convert.ToDateTime(orders.start);
                var end = Convert.ToDateTime(orders.end);
                    foreach (var o in order)
                    {
                        if (o.PlaceID== orders.PlaceID && o.Condition == "通过" && o.UseDate == orders.UseDate)
                        {
                            var s = Convert.ToDateTime(o.start);
                            var e = Convert.ToDateTime(o.end);
                            if (s <= start && start <= e)
                            {
                                ModelState.AddModelError("", "当前已经被预定。");
                                return View(orders);
                            }
                            if (s <= end && end <= e)
                            {
                                ModelState.AddModelError("", "当前已经被预定。");
                                return View(orders);
                            }
                        }
                    }
              
                    Order item = new Order();
                    item.UserID = WebSecurity.CurrentUserId;
                    item.OrderTime = DateTime.Now;
                    db.Order.Add(item);
                    db.SaveChanges();
                    var orderID = item.OrderID;
                    orders.OrderID = orderID;
                    orders.AdminID = 2;
                    orders.Condition = "待审批";
                    db.Order_Detail.Add(orders);
                    db.SaveChanges();
                    return RedirectToAction("info", "frame");
            }
            return View(orders);
        }
        //public ActionResult ManageOrder()
        //{

        //}

    }
}
