using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freedom.Models;

namespace Freedom.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
       public FreeDataEntities db = new FreeDataEntities();
    }
}
