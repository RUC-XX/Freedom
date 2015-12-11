using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.Runtime.Serialization.Json;
using Freedom.Models;

namespace Freedom.Controllers
{
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
        
        public ActionResult SaveEvents(UserEvent events)
        {
            events.UserID=WebSecurity.CurrentUserId;
            db.UserEvent.Add(events);
            db.SaveChanges();
            return RedirectToAction("getevents");
        }
        public ActionResult GetEvents()
        {
            List<UserEvent> eventlist = new List<UserEvent>();
            var events= from e in db.UserEvent
                        where e.UserID==WebSecurity.CurrentUserId
                        select e;
            foreach (var item in events){
                UserEvent newEvent = new UserEvent
                {
                    id = item.id,
                    title = item.title,
                    start = item.start,
                    end=item.end,
                    color=item.color,
                    textColor=item.textColor,
                    allDay=item.allDay
                };
                eventlist.Add(newEvent);
            }
            var rows = eventlist.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeDate(UserEvent events)
        {
            var Events = db.UserEvent.SingleOrDefault(m => m.id == events.id);
            Events.start = events.start;
            Events.end = events.end;
            db.SaveChanges();
            return RedirectToAction("getevents");
        }
        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        public ActionResult Reservation()
        {
            return View();
        }
       
        public ActionResult ShowActivity()
        {
            List<Place> place = new List<Place>();
            var places = from e in db.Place
                         select e;
            foreach (var item in places)
            {
                Place newplace = new Place
                {
                    PlaceID = item.PlaceID,
                    AreaID = item.AreaID,
                    PlaceNumber = item.PlaceNumber,
                    PlaceName = item.PlaceName,
                    PlaceSize = item.PlaceSize,
                };
                place.Add(newplace);
            }
            var rows = place.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowActivityByChange(Place places)
        {
            List<Place> place = new List<Place>();
            var p = from e in db.Place
                         where e.AreaName==places.AreaName
                         select e;
            foreach (var item in p)
            {
                Place newplace = new Place
                {
                    PlaceID = item.PlaceID,
                    AreaID = item.AreaID,
                    PlaceNumber = item.PlaceNumber,
                    PlaceName = item.PlaceName,
                    PlaceSize = item.PlaceSize,
                };
                place.Add(newplace);
            }
            var rows = place.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ShowActivityBySize(Place places)
        {
            List<Place> place = new List<Place>();
            var p = from e in db.Place
                    where e.PlaceSize == places.PlaceSize
                    select e;
            foreach (var item in p)
            {
                Place newplace = new Place
                {
                    PlaceID = item.PlaceID,
                    AreaID = item.AreaID,
                    PlaceNumber = item.PlaceNumber,
                    PlaceName = item.PlaceName,
                    PlaceSize = item.PlaceSize,
                };
                place.Add(newplace);
            }
            var rows = place.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);

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
