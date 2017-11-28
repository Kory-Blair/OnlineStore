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

        public ActionResult Index()
        {
            Purchase purchase = null;


            if (Request.Cookies.AllKeys.Contains("purchaseId"))
            {
                int purchaseId = int.Parse(Request.Cookies["purchaseId"].Value);
                
                purchase = db.Purchases.Find(purchaseId);
            }
            if (purchase == null)
            {
                purchase = new Purchase();
                db.Purchases.Add(purchase);
                db.SaveChanges();
                Response.AppendCookie(new HttpCookie("purchaseId", purchase.Id.ToString()));
            }

            return View(purchase);

        }
        [HttpPost]
        public ActionResult Index(Models.Purchase model)
        {

            if (Request.Cookies.AllKeys.Contains("purchaseId"))
            {
                int purchaseId = int.Parse(Request.Cookies["purchaseId"].Value);
                model = db.Purchases.Find(purchaseId);
                
            }

            if (model == null)
            {
                model = new Purchase();
                db.Purchases.Add(model);
                Response.AppendCookie(new HttpCookie("purchaseId", model.Id.ToString()));
            }
            
            

            return View(model);
        }
    }
}