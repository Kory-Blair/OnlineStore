using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class ReceiptController : Controller
    {
        protected OnlineStoreEntities db = new OnlineStoreEntities();

    public ActionResult Index(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Purchase purchase = db.Purchases.First(x => x.TrackingNumber == id);
        if (purchase == null)
        {
            return HttpNotFound();
        }
        return View(purchase);
    }


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
}
}