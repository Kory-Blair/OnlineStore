﻿using Braintree;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        PaymentService paymentService = new PaymentService();

        [Authorize]
        public ActionResult Index()
        {
            var customer = paymentService.GetCustomer(User.Identity.Name);
            return View(customer);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(string firstName, string lastName, string id)
        {
            Braintree.Customer customer = paymentService.UpdateCustomer(firstName, lastName, id);

            ViewBag.Message = "Updated Successfully";
            return View(customer);
        }

        public ActionResult Register()
        {
            return View();
        }

        [Authorize]
        public ActionResult Addresses()
        {
            var customer = paymentService.GetCustomer(User.Identity.Name);
            return View(customer.Addresses);
        }

        [Authorize]
        public ActionResult DeleteAddress(string id)
        {
            paymentService.DeleteAddress(User.Identity.Name, id);
            TempData["SuccessMessage"] = "Address deleted successfully";
            return RedirectToAction("Addresses");

        }

        [Authorize]
        [HttpPost]
        public ActionResult AddAddress(string firstName, string lastName, string company, string streetAddress, string extendedAddress, string locality, string region, string postalCode, string countryName)
        {

            paymentService.AddAddress(User.Identity.Name, firstName, lastName, company, streetAddress, extendedAddress, locality, region, postalCode, countryName);

            TempData["SuccessMessage"] = "Address added successfully";
            return RedirectToAction("Addresses");
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser { Email = username, UserName = username };
            IdentityResult result = userManager.Create(user, password);

            if (result.Succeeded)
            {
                
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, userIdentity);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = result.Errors;
            return View();
        }

        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string userName, string password, bool? staySignedIn)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByName(userName);
            
            if (user != null)
            {
                bool isPasswordValid = userManager.CheckPassword(user, password);
                if (isPasswordValid) { 

                var claimsIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                {
                    IsPersistent = staySignedIn ?? false,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, claimsIdentity);

                return RedirectToAction("Index", "Home");
            }
        }
         ViewBag.Error = new string[] { "Unable to sign in "};
        return View();
        }
        
    public ActionResult ForgotPassword()
{
    return View();
}

    [HttpPost]
    public ActionResult ForgotPassword(string email)
{
        var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
        var user = userManager.FindByEmail(email);
        if(user != null)
    {
                string resetToken = userManager.GeneratePasswordResetToken(user.Id);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword?email=" + email + "&token" + resetToken; 
                string message = string.Format("<a href=\"{0}\">Reset your password</a>", resetUrl);
                userManager.SendEmail(user.Id, "Your password reset token is", message);

    }
    return RedirectToAction("ForgotPasswordSent");
}

    public ActionResult ForgotPasswordSent()
{
    return View();
}

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string email, string token, string newPassword)
        {

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if (user != null)
            {
                IdentityResult result = userManager.ResetPassword(user.Id, token, newPassword);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Your password has been updated successfully";
                    return RedirectToAction("SignIn");
                }

            }
            return RedirectToAction("Index", "Home");
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
