using OnlineStore.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View(db.Services);
        }

        protected OnlineStoreEntities db = new OnlineStoreEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Details(int? id)
        {
            return View(db.Services.Find(id));
        }

        [HttpPost]
        public ActionResult Index(Service model)
        {
            
            Purchase purchase = null;
            if (Request.Cookies.AllKeys.Contains("purchaseId"))
            {

                int purchaseId = int.Parse(Request.Cookies["purchaseId"].Value);
                purchase = db.Purchases.Find(purchaseId);
            }
            if (purchase == null)
            {
                purchase = new Purchase
                {
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow, 
                };
                db.Purchases.Add(purchase);
                
                db.SaveChanges();
                Response.AppendCookie(new HttpCookie("purchaseId", purchase.Id.ToString()));
            }

            purchase.ServiceId = model.Id;
            purchase.RecurrenceId = null;
            purchase.Price = model.Price;
            db.SaveChanges();
            TempData.Clear();
            TempData.Add("NewItem", model.Name);

            
            return RedirectToAction("Index", "Cart");
        }
    }
}

