using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autotal.Factories;

namespace Autotal.Controllers
{
    public class UserController : Controller
    {
        DBContext context = new DBContext();

        public ActionResult Index()
        {
            return View();
        }
    }
}