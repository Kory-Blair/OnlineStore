using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {

        protected OnlineStoreEntities db = new OnlineStoreEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Cart
        public ActionResult Index()
        {
            Purchase purchase = null;
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                int cartID = int.Parse(Request.Cookies["cartID"].Value);
                purchase = db.Purchases.Find(cartID);
            }
            if (purchase == null)
            {
                purchase = new Purchase();
                db.Purchases.Add(purchase);
                Response.AppendCookie(new HttpCookie("cartID", purchase.Id.ToString()));
            }

            return View(purchase);
        }

        [HttpPost]
        public ActionResult Index(Models.Purchase model)
        {
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                int cartID = int.Parse(Request.Cookies["cartID"].Value);
                model = db.Purchases.Find(cartID);
            }
            if (model == null)
            {
                model = new Purchase();
                db.Purchases.Add(model);
                Response.AppendCookie(new HttpCookie("cartID", model.Id.ToString()));
            }
            return View(model);
        }
    }
}