using Braintree;
using CodingTemple.CodingCookware.Web;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Configuration;


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


            details.PriceModifer = details.CurrentCart.Recurrence.Price_Multiplier;
            details.CurrentCart.Price = details.CurrentCart.Service.Price * details.PriceModifer;
            details.CurrentCart.SubTotal = details.CurrentCart.Price;
            details.CurrentCart.Tax = details.CurrentCart.SubTotal * .1m;
            details.CurrentCart.Total = details.CurrentCart.SubTotal + details.CurrentCart.Tax;

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
                //decimal? total = model.CurrentCart.Total;
                //decimal? tax = model.CurrentCart.Tax;


                string message = payments.AuthorizeCard(model.ContactEmail, (model.CurrentCart.Total ?? .01m), (model.CurrentCart.Tax ?? 0m), model.TrackingNumber, model.ShippingAddress, model.CardholderName, model.CVV, model.CreditCardNumber, model.ExpirationMonth, model.ExpirationYear);

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

        [HttpPost]
        public ActionResult ValidateAddress(string street, string city, string state, string zip)
        {
            string authId = ConfigurationManager.AppSettings["SmartyStreets.AuthID"];
            string authToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];
            SmartyStreets.ClientBuilder clientBuilder = new SmartyStreets.ClientBuilder(authId, authToken);
            var client = clientBuilder.BuildUsStreetApiClient();
            SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup
            {
                City = city,
                ZipCode = zip,
                Street = street,
                State = state
            };

            client.Send(lookup);

            return Json(lookup.Result.Select(x => new
            {
                street = x.DeliveryLine1,
                city = x.Components.CityName,
                state = x.Components.State,
                zip = x.Components.ZipCode + "-" + x.Components.Plus4Code
            }));
        }
    }
}      