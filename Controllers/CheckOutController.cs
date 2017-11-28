using Braintree;
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
            int purchaseId = int.Parse(Request.Cookies["purchaseId"].Value);
            details.CurrentCart = db.Purchases.Find(purchaseId);
            //details.CurrentCart = null;
            details.Addresses = new Braintree.Address[0];
            if (User.Identity.IsAuthenticated)
            {
                string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
                string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
                string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
                string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                Braintree.BraintreeGateway gateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

                var customerGateway = gateway.Customer;
                Braintree.CustomerSearchRequest query = new Braintree.CustomerSearchRequest();
                query.Email.Is(User.Identity.Name);
                var matchedCustomers = customerGateway.Search(query);
                Braintree.Customer customer = null;

                if (matchedCustomers.Ids.Count == 0)
                {
                    Braintree.CustomerRequest Customer = new Braintree.CustomerRequest();
                    Customer.Email = User.Identity.Name;

                    var result = customerGateway.Create(Customer);
                    customer = result.Target;
                }
                else
                {
                    customer = matchedCustomers.FirstItem;
                }

                details.Addresses = customer.Addresses;
            }
            return View(details);
        }


        [HttpPost]
        public ActionResult Index(Models.CheckOut model, string addressId)
        {

            int purchaseId = int.Parse(Request.Cookies["purchaseId"].Value);
            model.CurrentCart = db.Purchases.Find(purchaseId);
            model.Addresses = new Braintree.Address[0];

            if (ModelState.IsValid)
            {
                model.ServiceName = model.CurrentCart.ServiceName;
                string TrackingNumber = Guid.NewGuid().ToString().Substring(0, 8);
                model.CurrentCart.TrackingNumber = TrackingNumber;
                model.CurrentCart.SubTotal = model.CurrentCart.Service.Price;
                model.CurrentCart.Tax = model.CurrentCart.SubTotal * .1m;
                model.CurrentCart.Total = model.CurrentCart.SubTotal + model.CurrentCart.Tax;


                PaymentService payments = new PaymentService();
                string email = User.Identity.IsAuthenticated ? User.Identity.Name : model.ContactEmail;
                decimal total = model.Total;
                decimal tax = model.Tax;


                string message = payments.AuthorizeCard(email, total, tax, model.TrackingNumber, addressId, model.CardholderName, model.CVV, model.CreditCardNumber, model.ExpirationMonth, model.ExpirationYear);

                if (string.IsNullOrEmpty(message))
                {
                    Recipient recipient = new Recipient
                    {
                        Email = model.ContactEmail,
                        Name = model.ContactName,
                        Address = model.ShippingAddress,
                        City = model.ShippingCity,
                        ZipCode = model.ShippingPostalCode,
                        State = model.ShippingState

                    };

                    Purchase purchase = new Purchase
                    {
                        DateCreated = DateTime.UtcNow,
                        DateLastModified = DateTime.UtcNow,
                        TrackingNumber = model.CurrentCart.TrackingNumber,
                        ShippingAndHandling = model.CurrentCart.ShippingAndHandling,
                        Tax = model.CurrentCart.Tax,
                        Total = model.CurrentCart.Total,
                        SubTotal = model.CurrentCart.SubTotal,

                    };
                    db.Purchases.Add(purchase);

                    db.SaveChanges();

                    OnlineStoreEmailService emailService = new OnlineStoreEmailService();
                    emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                    {
                        Subject = "Your receipt for order " + model.CurrentCart.TrackingNumber,
                        Destination = model.ContactEmail,
                        Body = "Thank you for shopping with us."
                    });

                    Response.SetCookie(new HttpCookie("purchaseId") { Expires = DateTime.UtcNow });

                    return RedirectToAction("Index", "Receipt", new { id = model.CurrentCart.TrackingNumber });
                }
                ModelState.AddModelError("CreditCardNumber", message);
            }
            return View(model);
        }
    }
}      