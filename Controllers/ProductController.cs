﻿using OnlineStore.Models;
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
            //TODO: Save the posted information to a database!
            Purchase purchase = null;
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                int cartID = int.Parse(Request.Cookies["cartID"].Value);
                purchase = db.Purchases.Find(cartID);
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
                Response.AppendCookie(new HttpCookie("cartID", purchase.Id.ToString()));
            }

            purchase.ServiceId = model.Id;
            purchase.Price = model.Price;
            db.SaveChanges();

            TempData.Add("NewItem", model.Name);

            //TODO: build up the cart controller!
            return RedirectToAction("Index", "Cart");
        }
    }
}

