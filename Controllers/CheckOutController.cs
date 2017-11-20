using CodingTemple.CodingCookware.Web;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class CheckoutController : Controller
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

            Models.CheckOut details = new Models.CheckOut();

            details.CurrentCart = null;
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                int cartID = int.Parse(Request.Cookies["cartID"].Value);
                details.CurrentCart = db.Purchases.Find(cartID);
            }
            if (details.CurrentCart == null)
            {
                details.CurrentCart = new Purchase();
                db.Purchases.Add(details.CurrentCart);
                Response.AppendCookie(new HttpCookie("cartID", details.CurrentCart.Id.ToString()));
            }

            return View(details);
        }

        
        [HttpPost]
        public ActionResult Index(Models.CheckOut model)
        {

            if (Request.Cookies.AllKeys.Contains("cartID"))
            {
                int cartID = int.Parse(Request.Cookies["cartID"].Value);
                model.CurrentCart = db.Purchases.Find(cartID);
            }
            if (model.CurrentCart == null)
            {
                model.CurrentCart = new Purchase();
                db.Purchases.Add(model.CurrentCart);
                Response.AppendCookie(new HttpCookie("cartID", model.CurrentCart.Id.ToString()));
            }


            if (ModelState.IsValid)
            {
                model.ServiceName = model.CurrentCart.ServiceName;
                string trackingNumber = Guid.NewGuid().ToString().Substring(0, 8);
                model.CurrentCart.TrackingNumber = trackingNumber;
                model.CurrentCart.SubTotal = model.CurrentCart.Service.Price;
                model.CurrentCart.Tax = model.CurrentCart.SubTotal * .1m;
                model.CurrentCart.Total = model.CurrentCart.SubTotal + model.CurrentCart.Tax;
                db.Recipients.Add(new Recipient
                {
                    
                    Email = model.ContactEmail,
                    ZipCode = model.ShippingPostalCode,
                    Address = model.ShippingAddress,
                    City = model.ShippingCity,
                    Name = model.ContactName,
                    State = model.ShippingState,
                    Purchase = model.CurrentCart
                });
                db.SaveChanges();
                Response.SetCookie(new HttpCookie("cartID") { Expires = DateTime.UtcNow });

                OnlineStoreEmailService emailService = new OnlineStoreEmailService();
                emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                    {

                    Subject = "Your Receipt for order" + trackingNumber,
                    Destination = model.ContactEmail,
                    Body = "Thank you for shopping!"
                        });

                return RedirectToAction("Index", "Receipt");
            }
            return View(model);
        }
    }
}