using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A little bit about ISRL:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Have some questions? Comments? Want to work for ITRL? Contact us!";

            return View();
        }
    }
}